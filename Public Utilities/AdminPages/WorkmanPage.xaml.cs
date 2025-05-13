using Public_Utilities.Add_Folder;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Public_Utilities.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для WorkmanPage.xaml
    /// </summary>
    public partial class WorkmanPage : Page
    {
        public WorkmanPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DB.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                WorkmanGrid.ItemsSource = DB.GetContext().Workman.ToList();
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Add_Workman add_Workman = new Add_Workman();
            add_Workman.Show();
        }
    }
}
