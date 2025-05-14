using Public_Utilities.Model;
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

namespace Public_Utilities.Edit_Folder
{
    /// <summary>
    /// Логика взаимодействия для Edit_Service.xaml
    /// </summary>
    public partial class Edit_Service : Window
    {
        private Services _currentService = new Services();
        public Edit_Service(Services selectedService)
        {
            InitializeComponent();
            DataContext = _currentService;

            if (_currentService != null)
                _currentService = selectedService;
            DataContext = _currentService;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentService.Service_ID == 0)
                DB.GetContext().Services.Add(_currentService);

            try
            {
                DB.GetContext().SaveChanges();
                MessageBox.Show("Данные изменены");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}