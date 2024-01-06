using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.OrganizerWPF.Mapper;
using Hotel.Presentation.OrganizerWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private readonly OrganizerManager _organizerManager;
        private readonly int _organizerId;
        private readonly string _organizerEmail;
        public ActivityWindow(OrganizerManager manager, int organizerId, string organizerEmail)
        {
            _organizerManager = manager;
            _organizerId = organizerId;
            InitializeComponent();
            _organizerEmail = organizerEmail;
        }

        private void AddActivityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int fixtureHours, fixtureMinutes;
                bool isValidHour = int.TryParse(txtFixtureHours.Text, out fixtureHours) && fixtureHours >= 0 && fixtureHours < 24;
                bool isValidMinute = int.TryParse(txtFixtureMinutes.Text, out fixtureMinutes) && fixtureMinutes >= 0 && fixtureMinutes < 60;

                if (!isValidHour || !isValidMinute)
                {
                    MessageBox.Show("Invalid time entered. Please enter hours (0-23) and minutes (0-59).", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!dpFixtureDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a date for the fixture.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime fixtureDateTime = dpFixtureDate.SelectedDate.Value.Date
                                           + new TimeSpan(fixtureHours, fixtureMinutes, 0);
                // Create an ActivityUI object with the input from the user
                ActivityUI activity = new ActivityUI()
                {
                    Name = txtName.Text,
                    Fixture = fixtureDateTime,
                    NrOfPlaces = int.Parse(txtNrOfPlaces.Text),
                    OrganizerId = _organizerId,
                    Duration = int.Parse(txtDuration.Text),
                    Location = txtLocation.Text,
                    Description = txtDescription.Text,
                    PriceAdult = int.Parse(txtPriceAdult.Text),
                    PriceChild = int.Parse(txtPriceChild.Text),
                    Discount = int.Parse(txtDiscount.Text)
                };

                // Convert ActivityUI to your domain's Activity object. This is pseudocode and needs to be replaced with actual mapping logic
                Activity domainActivity = UIToDomain.ActivityUIToActivity(activity);

                // Send to the manager (e.g., a service class responsible for handling activities). This is pseudocode and needs to be replaced with actual logic to persist the activity
                _organizerManager.AddActivity(domainActivity);
                
                MessageBox.Show("Activity added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                OrganizerUI organizer = DomainToUI.OrganizerToOrganizerUI(_organizerManager.GetOrganizerByEmail(_organizerEmail));
                OrganizerWindow organizerWindow = new OrganizerWindow(_organizerManager, organizer);
                this.Close();
                organizerWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UseDescription_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            OrganizerUI organizer = DomainToUI.OrganizerToOrganizerUI(_organizerManager.GetOrganizerByEmail(_organizerEmail));
            OrganizerWindow organizerWindow = new OrganizerWindow(_organizerManager, organizer);
            this.Close();
            organizerWindow.ShowDialog();
        }

    }


}
