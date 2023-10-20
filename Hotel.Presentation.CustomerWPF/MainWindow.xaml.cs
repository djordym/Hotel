﻿using Hotel.Domain.Managers;
using Hotel.Presentation.CustomerWPF.Model;
using Hotel.Util;
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


namespace Hotel.Presentation.CustomerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomerManager customerManager;
        private ObservableCollection<CustomerUI> customersUIs = new ObservableCollection<CustomerUI>();
        public MainWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            GetUpToDateCustomerInfo();
        }

        private void GetUpToDateCustomerInfo()
        {
            customersUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomersBy(null).Select(x => new CustomerUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString(), x.GetMembers().Count)));
            CustomerDataGrid.ItemsSource = customersUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerDataGrid.ItemsSource = new ObservableCollection<CustomerUI>(customerManager.GetCustomersBy(SearchTextBox.Text).Select(x => new CustomerUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString(), x.GetMembers().Count)));
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow w = new CustomerWindow(false, null);
            if (w.ShowDialog() == true)
            {
                GetUpToDateCustomerInfo();
            }

        }

        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("Please select a customer", "Delete");
            else
            {
                //customerManager.DeleteCustomer()
            }
        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("Please select a customer", "Update");
            else
            {
                CustomerWindow w = new CustomerWindow(true, (CustomerUI)CustomerDataGrid.SelectedItem);
                if(w.ShowDialog() == true)
                {
                    GetUpToDateCustomerInfo();
                }

            }
        }
    }
}