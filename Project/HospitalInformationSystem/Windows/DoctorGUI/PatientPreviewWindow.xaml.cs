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
            {
                if ((Patient)patientsTable.SelectedItem != null)
                    ShowPatientInformationWindow.GetInstance((Patient)patientsTable.SelectedItem).Show();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.T))
            {
                if ((Patient)patientsTable.SelectedItem != null)
                    if (((Patient)patientsTable.SelectedItem).hospitalTreatment != null)
                        EditHospitalTreatmentWindow.GetInstance((Patient)patientsTable.SelectedItem).Show();
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.F))
                searchTextBox.Focus();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => instance = null;

        private void patientsTable_PreviewKeyDown(object sender, KeyEventArgs e) => CheckKeyPress();

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
            foreach (Patient patient in PatientController.getInstance().getPatient())
            {
                if(patient.Name.Contains(searchTextBox.Text) || patient.Surname.Contains(searchTextBox.Text))
                {
                    patients.Add(patient);
                }    
            }
            patientsTable.ItemsSource = patients;
        }
    }
}
