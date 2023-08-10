using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace M7CarClientWpf
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        HttpClient client;
        UserInfo UserInfo { get; set; }
        public ProfileWindow(TokenModel token)
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5095");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
              new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);

            Task.Run(async () =>
            {
                UserInfo = await GetUserInfo();
            }).Wait();
            tb_firstname.Text = UserInfo.FirstName;
            tb_lastname.Text = UserInfo.LastName;
            tb_email.Text = UserInfo.Email;
            tb_username.Text = UserInfo.UserName;
            img.Source = ToImage(UserInfo.PhotoData);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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

        async Task<UserInfo> GetUserInfo()
        {
            var response = await client.GetAsync("auth");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UserInfo>();
            }
            throw new Exception("something wrong...");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "png files (*.png)|*.png|jpeg files (*.jpeg)|*.jpeg";
            if (ofd.ShowDialog() == true)
            {
                string filename = ofd.FileName;
                byte[] data = File.ReadAllBytes(filename);
                string contentType = MimeMapping.MimeUtility.GetMimeMapping(filename);
                img.Source = ToImage(data);
                UserInfo.PhotoData = data;
                UserInfo.PhotoContentType = contentType;
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RegisterViewModel model = new RegisterViewModel();
            model.FirstName = tb_firstname.Text;
            model.LastName = tb_lastname.Text;
            model.UserName = tb_username.Text;
            model.Email = tb_email.Text;
            model.Password = tb_password.Password;
            model.PhotoContentType = UserInfo.PhotoContentType;
            model.PhotoData = UserInfo.PhotoData;

            var response = await client.PostAsJsonAsync<RegisterViewModel>("auth/update", model);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Update succesful");
                this.DialogResult = true;
            }
        }
    }
}
