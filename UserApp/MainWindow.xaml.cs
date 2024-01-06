using Hotel.Domain.Managers;
using Hotel.Util;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserApp.Mapper;
using UserApp.Model;

namespace UserApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserManager _userManager;
        public MainWindow()
        {
            InitializeComponent();
            _userManager = new UserManager(RepositoryFactory.RegistrationRepository, RepositoryFactory.CustomerRepository);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerUI user = DomainToUI.MapCustomerToCustomerUI(_userManager.GetCustomerByEmail(EmailTextBox.Text));
                CustomerWindow userWindow = new CustomerWindow(user, _userManager);
                this.Close();
                userWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}