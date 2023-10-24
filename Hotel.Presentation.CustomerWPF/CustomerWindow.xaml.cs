using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.CustomerWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.Presentation.CustomerWPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerUI _customerUI;
        private bool _isUpdate;
        private CustomerManager _customermanager;

        public CustomerWindow(bool isUpdate, CustomerUI customerUI, CustomerManager customerManager)
        {
            InitializeComponent();
            _customerUI = customerUI;
            _isUpdate = isUpdate;
            _customermanager = customerManager;
            if (customerUI != null)
            {
                IdTextBox.Text = customerUI.Id.ToString();
                NameTextBox.Text = customerUI.Name;
                EmailTextBox.Text = customerUI.Email;
                PhoneTextBox.Text = customerUI.Phone;

                var match = Regex.Match(customerUI.Address, @"^(.*) \[(.*)\] - (.*) - (.*)$");
                if (match.Success)
                {

                    CityTextBox.Text = match.Groups[1].Value;
                    ZipTextBox.Text = match.Groups[2].Value;
                    StreetTextBox.Text = match.Groups[3].Value;
                    HouseNumberTextBox.Text = match.Groups[4].Value;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isUpdate)
            {
                _customerUI.Name = NameTextBox.Text;
                _customerUI.Email = EmailTextBox.Text;
                _customerUI.Phone = PhoneTextBox.Text;
                Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text );
                _customerUI.Address = address.ToString();

                _customermanager.UpdateCustomerInformation(_customerUI.Id, _customerUI.Name, _customerUI.Email, _customerUI.Phone, address.ToAddressLine());
                
            }
            else
            {
                Customer c = new Customer(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text)));
              //  customermanager.Add()
                c.Id = 100;
                //TODO Get Id from database
                _customerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count);
            }
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
