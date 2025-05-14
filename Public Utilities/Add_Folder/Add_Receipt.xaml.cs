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
    /// Логика взаимодействия для Add_Receipt.xaml
    /// </summary>
    public partial class Add_Receipt : Window
    {
        private Receipts _receipts = new Receipts();

        public Add_Receipt()
        {
            InitializeComponent();
            DataContext = _receipts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_receipts.Status))
                errors.AppendLine("Укажите статус");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_receipts.Receipt_ID == 0)
                DB.GetContext().Receipts.Add(_receipts);

            try
            {
                DB.GetContext().SaveChanges();
                MessageBox.Show("Квитанция создана");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
