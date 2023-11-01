using Hotel.Domain.Managers;
using Hotel.Presentation.CustomerWPF.Model;
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

namespace Hotel.Presentation.CustomerWPF
{
    /// <summary>
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {

        //private CustomerManager _customerManager;
        private MemberUI _member;
        private CustomerUI _customerUI;


        public MemberWindow(CustomerUI customerUI)
        {
            InitializeComponent();
            _member = new MemberUI();

            //_customerManager = customerManager;
            _customerUI = customerUI;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(BirthdayTextBox.Text))
            {
                MessageBox.Show("Please enter all fields.");
                return;
            }
            if (DateOnly.TryParse(BirthdayTextBox.Text, out DateOnly result));
            else
            {
                MessageBox.Show("Please enter valid date.");
                return;

            }


            _member.Name = NameTextBox.Text;
            _member.Birthday = result;
            _customerUI.Members.Add(_member);
            DialogResult = true;
            Close();
        }
    }
}
