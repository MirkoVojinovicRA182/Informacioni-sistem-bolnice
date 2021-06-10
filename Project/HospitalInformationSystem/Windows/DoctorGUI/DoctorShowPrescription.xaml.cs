using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static HospitalInformationSystem.Utility.Constants;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorShowPrescription.xaml
    /// </summary>
    public partial class DoctorShowPrescription : Window
    {
        private static DoctorShowPrescription instance = null;
        public static DoctorShowPrescription GetInstance(MedicalRecord patientsMedicalRecord)
        {
            if (instance == null)
                instance = new DoctorShowPrescription(patientsMedicalRecord);
            return instance;
        }
        private DoctorShowPrescription(MedicalRecord patientsMedicalRecord)
        {
            InitializeComponent();
            InitMedicineComboBox(patientsMedicalRecord);
        }
        public void InitMedicineComboBox(MedicalRecord patientsMedicalRecord)
        {
            medicineListBox.ItemsSource = patientsMedicalRecord.getPrescriptions();
        }
        private void medicineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startDateTextBox.Text = ((Prescription)medicineListBox.SelectedItem).startTime.ToString(DATE_TEMPLATE);
            endDateTextBox.Text = ((Prescription)medicineListBox.SelectedItem).endTime.ToString(DATE_TEMPLATE);
            infoTextBox.Text = ((Prescription)medicineListBox.SelectedItem).info;
        }
        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => instance = null;

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void medicineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startDateTextBox.Text = ((Prescription)medicineListBox.SelectedItem).startTime.ToString(DATE_TEMPLATE);
            endDateTextBox.Text = ((Prescription)medicineListBox.SelectedItem).endTime.ToString(DATE_TEMPLATE);
            infoTextBox.Text = ((Prescription)medicineListBox.SelectedItem).info;
        }
    }
}
