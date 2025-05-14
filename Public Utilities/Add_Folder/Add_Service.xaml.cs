using DocumentFormat.OpenXml.Spreadsheet;
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

namespace Public_Utilities.Add_Folder
{
    /// <summary>
    /// Логика взаимодействия для Add_Service.xaml
    /// </summary>
    public partial class Add_Service : Window
    {
        private Services _services = new Services();
        public Add_Service()
        {
            InitializeComponent();
            DataContext = _services;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_services.Service_Name))
                errors.AppendLine("Укажите услугу");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_services.Service_ID == 0)
                DB.GetContext().Services.Add(_services);

            try
            {
                DB.GetContext().SaveChanges();
                MessageBox.Show("Услуга добавлена");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
