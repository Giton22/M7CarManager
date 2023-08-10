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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        RegisterViewModel model = new RegisterViewModel();
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_password.Password != tb_password2.Password)
            {
                MessageBox.Show("Passwords not match!", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5095");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

                var response = await client.PutAsJsonAsync<RegisterViewModel>("auth", model);
                model.UserName = tb_username.Text;
                model.Password = tb_password.Password;
                model.FirstName = tb_firstname.Text;
                model.LastName = tb_lastname.Text;
                model.Email = tb_email.Text;

                if (response.IsSuccessStatusCode)
                {
                    var result = MessageBox.Show("Registration succesful", "Info", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    this.DialogResult = true;
                }
            }
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
                model.PhotoContentType = contentType;
                model.PhotoData = data;
            }
        }

        public BitmapImage ToImage(byte[] array)
        {
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
    }

    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoContentType { get; set; }
        public byte[] PhotoData { get; set; }
        public string Password { get; set; }

    }
}
