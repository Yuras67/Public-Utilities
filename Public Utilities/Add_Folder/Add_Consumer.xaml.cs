﻿using Public_Utilities.Model;
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
    /// Логика взаимодействия для Add_Consumer.xaml
    /// </summary>
    public partial class Add_Consumer : Window
    {
        private Сonsumers _сonsumers = new Сonsumers();
        public Add_Consumer()
        {
            InitializeComponent();
            DataContext = _сonsumers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_сonsumers.FullName))
                errors.AppendLine("Укажите ФИО");
            if (string.IsNullOrWhiteSpace(_сonsumers.Email))
                errors.AppendLine("Укажите почту");
            if (string.IsNullOrWhiteSpace(_сonsumers.Address))
                errors.AppendLine("Укажите адрес");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_сonsumers.Сonsumers_ID == 0)
                DB.GetContext().Сonsumers.Add(_сonsumers);

            try
            {
                DB.GetContext().SaveChanges();
                MessageBox.Show("Договор создан");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
