using CinemaManagement.DTOs;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CinemaManagement.Views.Staff
{
    public partial class MainStaffWindow : Window
    {
        public MainStaffWindow()
        {
            InitializeComponent();
        }

        private void mainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
        }
    }
}
