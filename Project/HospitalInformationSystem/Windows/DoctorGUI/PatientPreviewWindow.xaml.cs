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
        private Doctor _loggedDoctor;
        public static PatientPreviewWindow GetInstance(Doctor loggedDoctor)
        {
            if (instance == null)
                instance = new PatientPreviewWindow(loggedDoctor);
            return instance;
        }
        private PatientPreviewWindow(Doctor loggedDoctor)
        {
            InitializeComponent();
            this._loggedDoctor = loggedDoctor;
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
                ShowPatientInformationWindow.GetInstance((Patient)patientsTable.SelectedItem).Show();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.T))
            {
                if (((Patient)patientsTable.SelectedItem).hospitalTreatment != null)
                    EditHospitalTreatmentWindow.GetInstance((Patient)patientsTable.SelectedItem).Show();
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => instance = null;

        private void patientsTable_PreviewKeyDown(object sender, KeyEventArgs e) => CheckKeyPress();
    }
}
