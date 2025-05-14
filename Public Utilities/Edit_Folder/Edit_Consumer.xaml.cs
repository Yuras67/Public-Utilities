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
    /// Логика взаимодействия для Edit_Consumer.xaml
    /// </summary>
    public partial class Edit_Consumer : Window
    {
        private Сonsumers _currentConsumers = new Сonsumers();
        public Edit_Consumer(Сonsumers selectedConsumers)
        {
            InitializeComponent();
            DataContext = _currentConsumers;

            if (_currentConsumers != null)
                _currentConsumers = selectedConsumers;
            DataContext = _currentConsumers;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentConsumers.FullName))
                errors.AppendLine("Укажите ФИО");
            if (string.IsNullOrWhiteSpace(_currentConsumers.Email))
                errors.AppendLine("Укажите почту");
            if (string.IsNullOrWhiteSpace(_currentConsumers.Address))
                errors.AppendLine("Укажите адрес");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentConsumers.Сonsumers_ID == 0)
                DB.GetContext().Сonsumers.Add(_currentConsumers);

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
