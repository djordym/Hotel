using Hotel.Domain.Managers;
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
using System.Windows.Shapes;
using UserApp.Model;

namespace UserApp
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private UserManager _userManager;
        public CustomerWindow(CustomerUI customer, UserManager manager)
        {
            InitializeComponent();
            _userManager = manager;
            CustomerUI = customer;
        }

        private CustomerUI _customerUI { get; set; }
        public CustomerUI CustomerUI
        {
            get { return _customerUI; }
            set
            {
                _customerUI = value;
                DataContext = _customerUI;
            }
        }

        public ObservableCollection<RegistrationUI> Registrations { get; set; }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RegistrationDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }



    }
}
