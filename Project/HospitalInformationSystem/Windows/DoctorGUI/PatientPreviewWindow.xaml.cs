using HospitalInformationSystem.Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for PatientPreviewWindow.xaml
    /// </summary>
    public partial class PatientPreviewWindow : Window
    {
        private static PatientPreviewWindow instance = null;
        private Doctor loggedInDoctor;
        public static PatientPreviewWindow GetInstance(Doctor loggedInDoctor)
        {
            if (instance == null)
                instance = new PatientPreviewWindow(loggedInDoctor);
            return instance;
        }
        private PatientPreviewWindow(Doctor loggedInDoctor)
        {
            InitializeComponent();
            this.loggedInDoctor = loggedInDoctor;
            initPatientsTable();
        }

        private void initPatientsTable()
        {
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
            foreach (Patient patient in PatientController.getInstance().getPatient())
            {
                patients.Add(patient);
            }
            patientsTable.ItemsSource = patients;
        }

        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                ShowPatientInformationWindow window = ShowPatientInformationWindow.GetInstance((Patient)patientsTable.SelectedItem, loggedInDoctor);
                window.Show();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.T))
            {
                if (((Patient)patientsTable.SelectedItem).hospitalTreatment != null)
                {
                    EditHospitalTreatmentWindow window = EditHospitalTreatmentWindow.GetInstance((Patient)patientsTable.SelectedItem);
                    window.Show();
                }
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void patientsTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
