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
    /// Логика взаимодействия для Add_Contract.xaml
    /// </summary>
    public partial class Add_Contract : Window
    {
        private Contracts _contracts = new Contracts();

        public Add_Contract()
        {
            InitializeComponent();
            DataContext = _contracts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_contracts.Organization))
                errors.AppendLine("Укажите организацию");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_contracts.Contract_ID == 0)
                DB.GetContext().Contracts.Add(_contracts);

            try
            {
                DB.GetContext().SaveChanges();
                MessageBox.Show("Договор создан");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
