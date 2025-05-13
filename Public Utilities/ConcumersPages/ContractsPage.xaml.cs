using Public_Utilities.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    public partial class ContractsPage : Page
    {
        public ContractsPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadUserContracts();
        }


        public class Contract
        {
            public int Contract_ID { get; set; }
            public int Service_ID { get; set; }
            public int Consumer_ID { get; set; }
            public string Organization { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }

        public class Service
        {
            public int Service_ID { get; set; }
            public string Service_Name { get; set; }
        }

        public class ContractView
        {
            public int Contract_ID { get; set; }
            public string Service_Name { get; set; }
            public int Consumer_ID { get; set; }
            public string Organization { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }

        public ObservableCollection<ContractView> Contracts { get; set; }

        public void LoadUserContracts()
        {
            var userId = Session.CurrentUserId;

            using (var dbContext = new DB())
            {
                var contracts = dbContext.Contracts
                    .Where(c => c.Сonsumers_ID == userId)
                    .ToList();

                var services = dbContext.Services.ToList();

                var joinedData = from contract in contracts
                                 join service in services
                                 on contract.Service_ID equals service.Service_ID
                                 select new ContractView
                                 {
                                     Contract_ID = contract.Contract_ID,
                                     Service_Name = service.Service_Name,
                                     Organization = contract.Organization,
                                     Description = contract.Description,
                                     Price = contract.Price
                                 };

                Contracts = new ObservableCollection<ContractView>(joinedData);
            }
        }
    }
}
