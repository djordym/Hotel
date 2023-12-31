﻿using Hotel.Domain.Managers;
using Hotel.Presentation.OrganizerWPF.Mapper;
using Hotel.Presentation.OrganizerWPF.Model;
using Hotel.Util;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel.Presentation.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OrganizerManager _organizerManager;
        public MainWindow()
        {
            InitializeComponent();
            _organizerManager = new OrganizerManager(RepositoryFactory.OrganizerRepository);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            OrganizerUI organizer = DomainToUI.OrganizerToOrganizerUI(_organizerManager.GetOrganizerByEmail(EmailTextBox.Text));
            OrganizerWindow organizerWindow = new OrganizerWindow(_organizerManager, organizer);
            this.Close();
            organizerWindow.ShowDialog();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
