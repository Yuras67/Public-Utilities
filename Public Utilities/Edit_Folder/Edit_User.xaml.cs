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
    /// Логика взаимодействия для Edit_User.xaml
    /// </summary>
    public partial class Edit_User : Window
    {
        private Users _currentUser = new Users();
        public Edit_User(Users selectedUser)
        {
            InitializeComponent();
            DataContext = _currentUser;

            if (_currentUser != null)
                _currentUser = selectedUser;
            DataContext = _currentUser;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUser.User_ID == 0)
                DB.GetContext().Users.Add(_currentUser);

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