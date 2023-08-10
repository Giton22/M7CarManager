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
using WpfSocialLogin;

namespace M7CarClientWpf
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void LoginWithFb(object sender, MouseButtonEventArgs e)
        {
            FbLoginWindow flw = new FbLoginWindow();
            if (flw.ShowDialog() == true)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5095");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
                var response = await client.PostAsJsonAsync<SocialToken>("auth/Facebook", new SocialToken()
                {
                    Token = flw.AccessToken
                });
                var identityToken = await response.Content.ReadAsAsync<TokenModel>();
                MainWindow mw = new MainWindow(identityToken);
                mw.ShowDialog();

            }
        }

        private void LoginWithO365(object sender, MouseButtonEventArgs e)
        {
            O365LoginWindow olw = new O365LoginWindow();
            if (olw.ShowDialog() == true)
            {
                MessageBox.Show(olw.AccessToken);
                File.WriteAllText("o365.txt", olw.AccessToken);
            }
        }

        private void LoginWithGoogle(object sender, MouseButtonEventArgs e)
        {
            GoogleLoginWindow olw = new GoogleLoginWindow();
            if (olw.ShowDialog() == true)
            {
                MessageBox.Show(olw.AccessToken);
                File.WriteAllText("google.txt", olw.AccessToken);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5095");
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            var response = await client.PostAsJsonAsync<LoginViewModel>("auth", new LoginViewModel()
            {
                UserName = tb_username.Text,
                Password = tb_password.Password
            });

            var token = await response.Content.ReadAsAsync<TokenModel>();
            token.Expiration = token.Expiration.ToLocalTime();

            MainWindow mw = new MainWindow(token);
            mw.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.ShowDialog();
        }
    }

    internal class SocialToken
    {
        public string Token { get; set; }
    }

    public class TokenModel
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }

    internal class LoginViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
