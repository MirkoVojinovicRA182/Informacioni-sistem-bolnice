using Model;
using System.Windows;
using System.Windows.Input;

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

        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                MedicalRecordWindow medicalRecordWindow = new MedicalRecordWindow(patient);
                medicalRecordWindow.ShowDialog();
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CheckKeyPress();
        }

        /*private void newAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 doctorAddNewAppointmentWindow = new Window2(doctor);

            doctorAddNewAppointmentWindow.ShowDialog();
            doctorAddNewAppointmentWindow.patientListBox.SelectedIndex = 10;
            doctorAddNewAppointmentWindow.patientListBox.SelectedItem = patient;
        }*/
    }
}
