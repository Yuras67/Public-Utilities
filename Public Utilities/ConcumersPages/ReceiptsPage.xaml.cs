using Public_Utilities.Add_Folder;
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
            DataContext = this;
            LoadUserContracts();
        }


        public class ReceiptView
        {
            public int Receipt_ID { get; set; }
            public int Contract_ID { get; set; }
            public string Address { get; set; }
            public DateTime Date { get; set; }
            public string PaymentAmount { get; set; }
            public string Status { get; set; }
        }

        public ObservableCollection<ReceiptView> Receipts { get; set; }

        public void LoadUserContracts()
        {
            var userId = Session.CurrentUserId;

            using (var dbContext = new DB())
            {
                var receipts = dbContext.Receipts
                    .Where(c => c.Сonsumers_ID == userId)
                    .ToList();

                var consumers = dbContext.Сonsumers.ToList();

                var joinedData = from receipt in receipts
                                 join consumer in consumers
                                 on receipt.Сonsumers_ID equals consumer.Сonsumers_ID
                                 select new ReceiptView
                                 {
                                     Receipt_ID = receipt.Receipt_ID,
                                     Contract_ID = receipt.Contract_ID,
                                     Address = consumer.Address,
                                     Date = receipt.Date,
                                     PaymentAmount = receipt.PaymentAmount,
                                     Status = receipt.Status
                                 };

                Receipts = new ObservableCollection<ReceiptView>(joinedData);
            }
        }
    }
}
