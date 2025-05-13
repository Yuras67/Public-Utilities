using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Public_Utilities.Windows
{
    /// <summary>
    /// Логика взаимодействия для BasedWindow.xaml
    /// </summary>
    public partial class Consumer_Window : Window
    {
        public Consumer_Window(int user_id)
        {
            InitializeComponent();
            Session.CurrentUserId = user_id;
        }

        public static class Session
        {
            public static int CurrentUserId { get; set; }
        }


        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void Open_Contracts(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ConcumersPages.ContractsPage());
        }

        private void Open_Receipts(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ConcumersPages.ReceiptsPage());
        }
    }
}
