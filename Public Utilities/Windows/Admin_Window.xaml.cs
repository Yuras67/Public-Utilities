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

namespace Public_Utilities.Windows
{
    /// <summary>
    /// Логика взаимодействия для Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        public Admin_Window()
        {
            InitializeComponent();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void Open_Users(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPages.UsersPage());
        }

        private void Open_Contracts(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPages.ContractsPage());
        }

        private void Open_Receipts(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPages.ReceiptsPage());
        }

        private void Open_Services(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPages.ServicesPage());
        }

        private void Open_Workman(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPages.WorkmanPage());
        }

        private void Open_Consumers(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPages.СonsumersPage());
        }
    }
}
