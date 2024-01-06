using Hotel.Domain.Managers;
using Hotel.Presentation.OrganizerWPF.Model;
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

namespace Hotel.Presentation.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for OrganizerWindow.xaml
    /// </summary>
    public partial class OrganizerWindow : Window
    {
        private readonly OrganizerManager _organizerManager;
        public OrganizerWindow(OrganizerManager manager, OrganizerUI organizer)
        {
            _organizerManager = manager;
            Organizer = organizer;
            InitializeComponent();
        }

        private OrganizerUI _organizerUI;
        OrganizerUI Organizer
        {
            get { return _organizerUI; }
            set
            {
                _organizerUI = value;
                DataContext = _organizerUI;
            }
        }




        private void AddActivityButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityWindow activityWindow = new ActivityWindow(_organizerManager, _organizerUI.Id, _organizerUI.Email);
            this.Close();
            activityWindow.ShowDialog();
        }

        private void DeleteActivityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            ActivityUI activityUI = (ActivityUI)ActivityDataGrid.SelectedItem;
            _organizerUI.Activities.Remove(activityUI);
            _organizerManager.RemoveActivityById(activityUI.Id);
            } catch (Exception)
            {
                MessageBox.Show("Please select an activity to delete");
            }
        }

        //when you close window go back to login window
        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
