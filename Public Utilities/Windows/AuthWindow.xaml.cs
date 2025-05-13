using Public_Utilities.Model;
using Public_Utilities.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace Public_Utilities
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public static AuthWindow window;
        private string captchaValue;


        public AuthWindow()
        {
            InitializeComponent();

            window = this;
        }

        private void Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                AuthWindow.window.DragMove();
            }
        }

        private void Click_Next(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            string login = Log.Text;
            string password = Pass.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Все поля обязательны для заполнения");
                GenerateCaptcha();
                return;
            }

            var currentUser = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);


            if (currentUser == null)
            {
                MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
                GenerateCaptcha();
                return;
            }

            if (CaptchaTextBox.Text.Equals(captchaValue, StringComparison.OrdinalIgnoreCase) || CaptchaTextBox.Text == "")
            {
                if (currentUser.Role == "Admin")
                {
                    Admin_Window admin_Window = new Admin_Window();
                    this.Close();
                    admin_Window.Show();
                }
                else if (currentUser.Role == "Consumer")
                {
                    Consumer_Window consumers_window = new Consumer_Window(currentUser.User_ID);
                    this.Close();
                    consumers_window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
                    GenerateCaptcha();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Вы ввели неверную капчу. Пожалуйста проверьте ещё раз");
                GenerateCaptcha();
                return;
            }

        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowCaptcha()
        {
            CaptchaPanel.Visibility = Visibility.Visible;
            CaptchaTextBlock.Text = captchaValue;
        }

        private void GenerateCaptcha()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            StringBuilder captchaStringBuilder = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                captchaStringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            captchaValue = captchaStringBuilder.ToString();
            ShowCaptcha();
        }

        private void Update_Captcha_Click(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
        }
    }
}
