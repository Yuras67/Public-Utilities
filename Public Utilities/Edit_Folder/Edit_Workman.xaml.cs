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
    /// Логика взаимодействия для Edit_Workman.xaml
    /// </summary>
    public partial class Edit_Workman : Window
    {
        private Workman _currentWorkman = new Workman();
        public Edit_Workman(Workman selectedWorkman)
        {
            InitializeComponent();
            DataContext = _currentWorkman;

            if (_currentWorkman != null)
                _currentWorkman = selectedWorkman;
            DataContext = _currentWorkman;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentWorkman.Workman_ID == 0)
                DB.GetContext().Workman.Add(_currentWorkman);

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
