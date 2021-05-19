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
        private Doctor doctor;
        private Patient patient;
        private static ShowPatientInformationWindow instance = null;
        public static ShowPatientInformationWindow GetInstance(Patient patient, Doctor doctor)
        {
            if (instance == null)
                instance = new ShowPatientInformationWindow(patient, doctor);
            return instance;
        }
        private ShowPatientInformationWindow(Patient patient, Doctor doctor)
        {
            InitializeComponent();
            this.patient = patient;
            this.doctor = doctor;
            nameLabel.Content = patient.Name + " " + patient.Surname;
            jmbgLabel.Content = patient.Jmbg;
        }

        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.Z))
            {
                MedicalRecordWindow medicalRecordWindow = new MedicalRecordWindow(patient);
                medicalRecordWindow.ShowDialog();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.T))
            {
                SendToHospitalTreatmentWindow window = SendToHospitalTreatmentWindow.GetInstance(patient);
                window.Show();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
