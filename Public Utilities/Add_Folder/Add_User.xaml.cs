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
    /// Логика взаимодействия для Add_User.xaml
    /// </summary>
    public partial class Add_User : Window
    {
        private Users _users = new Users();
        public Add_User()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_users.Login))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrWhiteSpace(_users.Password))
                errors.AppendLine("Укажите пароль");
            if (string.IsNullOrWhiteSpace(_users.Role))
                errors.AppendLine("Укажите роль");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_users.User_ID == 0)
                DB.GetContext().Users.Add(_users);

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
