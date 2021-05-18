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
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {
        private Patient loggedInPatient;
        private static PatientMainWindow instance = null;
        public PatientMainWindow(Patient patient)
        {
            InitializeComponent();
            loggedInPatient = patient;
        }
 
        public static PatientMainWindow GetInstance(Patient patient)
        {
            if (instance == null)
                instance = new PatientMainWindow(patient);
            return instance;
        }

        private void AppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAppointmentCRUDOperationsWindow window = new PatientAppointmentCRUDOperationsWindow(loggedInPatient);
            window.Show();
            this.Hide();
        }

        private void TherapiesButton_Click(object sender, RoutedEventArgs e)
        {
            PatientTherapiesWindow window = new PatientTherapiesWindow(loggedInPatient);
            window.Show();
            this.Hide();
        }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationsWindow window = new NotificationsWindow(loggedInPatient);
            window.Show();
            this.Hide();
        }

        private void HospitalReviewButton_Click(object sender, RoutedEventArgs e)
        {
            ReviewHospitalWindow window = new ReviewHospitalWindow(loggedInPatient);
            window.Show();
            this.Hide();
        }
        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void AnamnesisButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAnamnesisWindow window = new PatientAnamnesisWindow(loggedInPatient);
            window.Show();
        }
    }
}
