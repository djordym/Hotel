using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.CustomerWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            
            if (customerUI == null) //when new customer set up customerUI for use
            {
                _customerUI = new CustomerUI();
                MemberDataGrid.ItemsSource = _customerUI.Members;
            }
            else
            {
                _customerUI = customerUI;
                IdTextBox.Text = customerUI.Id.ToString();
                NameTextBox.Text = customerUI.Name;
                EmailTextBox.Text = customerUI.Email;
                PhoneTextBox.Text = customerUI.Phone;
                MemberDataGrid.ItemsSource = customerUI.Members;
                var match = Regex.Match(customerUI.Address, @"^(.*) \[(.*)\] - (.*) - (.*)$");
                if (match.Success)
                {

                    CityTextBox.Text = match.Groups[1].Value;
                    ZipTextBox.Text = match.Groups[2].Value;
                    StreetTextBox.Text = match.Groups[3].Value;
                    HouseNumberTextBox.Text = match.Groups[4].Value;
                }
            }
            
            _isUpdate = isUpdate;
            _customermanager = customerManager;
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);

            if (_isUpdate)
            {
                Customer c = new Customer(NameTextBox.Text, int.Parse(IdTextBox.Text), new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, address));
                
                AddMembersToCustomer(_customerUI.Members, c);

                UpdateCustomerUI(NameTextBox.Text, EmailTextBox.Text, PhoneTextBox.Text, address);


                _customermanager.UpdateCustomer(c);

            }
            else
            {
                Customer c = new Customer(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, address));

                AddMembersToCustomer(_customerUI.Members, c);

                _customermanager.AddCustomer(c);

                UpdateCustomerUI(c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, address);
                _customerUI.NrOfMembers = c.GetMembers().Count;
            }

            DialogResult = true;
            Close();


            //if (_isUpdate) 
            //{
            //    _customerUI.Name = NameTextBox.Text;
            //    _customerUI.Email = EmailTextBox.Text;
            //    _customerUI.Phone = PhoneTextBox.Text;
            //    Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text );
            //    _customerUI.Address = address.ToString();

            //    Customer c = new Customer(NameTextBox.Text, int.Parse(IdTextBox.Text), new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, address));
            //    foreach (MemberUI mUI in _customerUI.Members)
            //    {
            //        Member m = new Member(mUI.Name, mUI.Birthday);
            //        c.AddMember(m);
            //    }
            //    _customermanager.UpdateCustomer(c);

            //}
            //else
            //{

            //    Customer c = new Customer(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text)));
            //    foreach(MemberUI mUI in _customerUI.Members)
            //    {
            //        Member m = new Member(mUI.Name, mUI.Birthday);
            //        c.AddMember(m);
            //    }

            //    _customermanager.AddCustomer(c);

            //    c.Id = _customermanager.GetCustomerIdByEmail(EmailTextBox.Text);

            //    _customerUI.Id = c.Id;
            //    _customerUI.Name = c.Name;
            //    _customerUI.Email = c.ContactInfo.Email;
            //    _customerUI.Phone = c.ContactInfo.Phone;
            //    _customerUI.Address = c.ContactInfo.Address.ToString();
            //    _customerUI.NrOfMembers = c.GetMembers().Count;

            //}
            //DialogResult = true;
            //Close();
        }

        void UpdateCustomerUI(string name, string email, string phone, Address address)
        {
            _customerUI.Name = name;
            _customerUI.Email = email;
            _customerUI.Phone = phone;
            _customerUI.Address = address.ToString();
            _customerUI.Id = _customermanager.GetCustomerIdByEmail(email);
        }

        

        

        void AddMembersToCustomer(IEnumerable<MemberUI> memberUIs, Customer c)
        {
            foreach (MemberUI mUI in memberUIs)
            {
                Member m = new Member(mUI.Name, mUI.Birthday);
                c.AddMember(m);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemAddMember_Click(object sender, RoutedEventArgs e)
        {
            
            MemberWindow w = new MemberWindow(_customerUI);
            w.ShowDialog();
            
        }

        private void MenuItemDeleteMember_Click(Object sender, RoutedEventArgs e)
        {

        }
    }
}
