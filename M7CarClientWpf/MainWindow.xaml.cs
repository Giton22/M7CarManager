using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M7CarClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Car> Cars { get; set; }

        private Car actualCar;
        UserInfo UserInfo { get; set; }

        HttpClient client;
        HubConnection conn;

        TokenModel tokenModel;
        public Car ActualCar
        {
            get { return actualCar; }
            set { actualCar = value?.GetCopy(); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActualCar")); }
        }

        public MainWindow(TokenModel token)
        {
            
            InitializeComponent();
            this.ActualCar = new Car();

            this.tokenModel = token;

            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5095");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
              new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);


            Task.Run(async() =>
            {
                Cars = new ObservableCollection<Car>(await GetCars());
                UserInfo = await GetUserInfo();
            }).Wait();

            lb_profilename.Content = UserInfo.FirstName + " " + UserInfo.LastName;
            img_profilephoto.Source = ToImage(UserInfo.PhotoData);

            conn = new HubConnectionBuilder().WithUrl("http://localhost:5095/events").Build();
            conn.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await conn.StartAsync();
            };

            conn.On<Car>("carCreated", async t => await Refresh());
            conn.On<Car>("carModified", async t => await Refresh());
            conn.On<string>("carDeleted", t =>
            {
                var carToDelete = Cars.FirstOrDefault(z => z.Id == t);
                this.Dispatcher.Invoke(() =>
                {
                    Cars.Remove(carToDelete);
                });
            });

            Task.Run(async () =>
            {
                await conn.StartAsync();
            }).Wait();

            this.DataContext = this;
        }

        async Task Refresh()
        {
            Cars = new ObservableCollection<Car>(await GetCars());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cars"));
        }

        async Task<IEnumerable<Car>> GetCars()
        {
            var response = await client.GetAsync("/car");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Car>>();
            }
            throw new Exception("something wrong...");
        }

        async Task<UserInfo> GetUserInfo()
        {
            var response = await client.GetAsync("auth");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UserInfo>();
            }
            throw new Exception("something wrong...");
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            var response = await client.DeleteAsync("/car/" + ActualCar.Id);
            response.EnsureSuccessStatusCode();
            //await Refresh();
        }

        public BitmapImage ToImage(byte[] array)
        {
            if (array == null)
            {
                return new BitmapImage();
            }
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private async void Create(object sender, RoutedEventArgs e)
        {
            this.ActualCar.Id = "";
            var response = await client.PostAsJsonAsync("/car/AddCar", this.ActualCar);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var error = await response.Content.ReadAsAsync<ErrorModel>();
                MessageBox.Show(error.Message + " at: " + error.Date.ToShortTimeString(), "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //await Refresh();
            }
            
        }

        private async void Update(object sender, RoutedEventArgs e)
        {
            var response = await client.PutAsJsonAsync("/car", this.ActualCar);
            response.EnsureSuccessStatusCode();
            //await Refresh();
        }

        private async void lb_profilename_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProfileWindow pw = new ProfileWindow(tokenModel);
            if (pw.ShowDialog() == true)
            {
                UserInfo = await GetUserInfo();
                lb_profilename.Content = UserInfo.FirstName + " " + UserInfo.LastName;
                img_profilephoto.Source = ToImage(UserInfo.PhotoData);
            }
        }
    }

    public class UserInfo
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoContentType { get; set; }
        public byte[] PhotoData { get; set; }
        public List<string> Roles { get; set; }

    }
}


