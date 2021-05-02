using Model;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for ShowPatientInformationWindow.xaml
    /// </summary>
    public partial class ShowPatientInformationWindow : Window
    {
        Doctor doctor;
        Patient patient;

        public ShowPatientInformationWindow(Patient patient, Doctor doctor)
        {
            InitializeComponent();
            this.patient = patient;
            this.doctor = doctor;
            nameLabel.Content = patient.Name + " " + patient.Surname;
            jmbgLabel.Content = patient.Jmbg;
        }

        private void medicalRecordButton_Click(object sender, RoutedEventArgs e)
        {

            MedicalRecordWindow medicalRecordWindow = new MedicalRecordWindow(patient);

            medicalRecordWindow.ShowDialog();
        }

        private void newAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 doctorAddNewAppointmentWindow = new Window2(doctor);

            doctorAddNewAppointmentWindow.ShowDialog();
            doctorAddNewAppointmentWindow.patientComboBox.SelectedIndex = 10;
            doctorAddNewAppointmentWindow.patientComboBox.SelectedItem = patient;
        }
    }
}
