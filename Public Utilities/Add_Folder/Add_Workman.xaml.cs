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
    /// Логика взаимодействия для Add_Workman.xaml
    /// </summary>
    public partial class Add_Workman : Window
    {
        private Workman _workman = new Workman();
        public Add_Workman()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_workman.FullName))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrWhiteSpace(_workman.Email))
                errors.AppendLine("Укажите пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_workman.Workman_ID == 0)
                DB.GetContext().Workman.Add(_workman);

            try
            {
                DB.GetContext().SaveChanges();
                MessageBox.Show("Пользователь создан");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
