using Public_Utilities.Model;
using Public_Utilities.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static Public_Utilities.Windows.Consumer_Window;

namespace Public_Utilities.ConcumersPages
{
    /// <summary>
    /// Логика взаимодействия для ReceiptsPage.xaml
    /// </summary>
    public partial class ReceiptsPage : Page
    {
        public ReceiptsPage()
        {
            InitializeComponent();
            ReceiptsGrid.ItemsSource = DB.GetContext().Receipts.ToList();
        }

    }
}
