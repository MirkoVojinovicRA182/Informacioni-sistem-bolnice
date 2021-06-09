using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Service;
using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for EmergentSurgeryPage.xaml
    /// </summary>
    public partial class EmergentSurgeryPage : Page
    {
        private ObservableCollection<Appointment> _appointmentsList = new ObservableCollection<Appointment>();
        public EmergentSurgeryPage()
        {
            InitializeComponent();
        }

        private void patientCmb_Loaded(object sender, RoutedEventArgs e)
        {
            patientCmb.ItemsSource = PatientController.getInstance().getPatient();
        }

        private void specilizationCmb_Loaded(object sender, RoutedEventArgs e)
        {
            specilizationCmb.ItemsSource = Enum.GetValues(typeof(Specialization)).Cast<Specialization>();
        }

        private void AppointmentsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            appointmentsGrid.ItemsSource = _appointmentsList;
        }

        private void SpecilizationCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _appointmentsList.Clear();
            if (patientCmb.SelectedIndex != -1)
                SecretaryUtil.FindNearestAppointments(_appointmentsList, (Specialization)specilizationCmb.SelectedItem, (Patient)patientCmb.SelectedItem);
        }
        private void PatientCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _appointmentsList.Clear();
            if (specilizationCmb.SelectedIndex != -1)
                SecretaryUtil.FindNearestAppointments(_appointmentsList, (Specialization)specilizationCmb.SelectedItem, (Patient)patientCmb.SelectedItem);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentController.getInstance().AddAppointmentToAppointmentList(_appointmentsList.First());
        }
    }
}
