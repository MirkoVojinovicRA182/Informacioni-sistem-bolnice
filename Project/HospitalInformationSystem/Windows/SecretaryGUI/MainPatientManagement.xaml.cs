using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Windows.SecretaryGUI;
using Model;
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
using System.IO;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for MainPatientManagement.xaml
    /// </summary>
    public partial class MainPatientManagement : Window
    {
        private static MainPatientManagement instance = null;
        private MainPatientManagement()
        {
            InitializeComponent();
        }
        public static MainPatientManagement Instance
        {
            get {
                if (instance == null)
                {
                    instance = new MainPatientManagement();
                }
                return instance;
            }
        }

        private void NaloziBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AccountsPage();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.Serialize();
            instance = null;
        }

        private void VestiBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AnnouncementsPage();
        }

        private void AllergensBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AllergensPage();
        }

        private void emergentSurgeryBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EmergentSurgeryPage();
        }

        private void doctorsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DoctorsCRUDPage();
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ReportPage();
        }

        private void feedbackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new FeedbackPage();
        }
    }
}
