using Model;
using System.Windows;
using System.Windows.Controls;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorShowPrescription.xaml
    /// </summary>
    public partial class DoctorShowPrescription : Window
    {

        private MedicalRecord medicalRecord;
        public DoctorShowPrescription(MedicalRecord medicalRecord)
        {
            InitializeComponent();
            this.medicalRecord = medicalRecord;
            initData();
        }

        public void initData()
        {
            medicineComboBox.ItemsSource = medicalRecord.getPrescriptions();
        }

        private void medicineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Prescription prescription = (Prescription)medicineComboBox.SelectedItem;
            startDateTextBox.Text = prescription.startTime.ToString("dd.MM.yyyy.");
            endDateTextBox.Text = prescription.endTime.ToString("dd.MM.yyyy.");
            infoTextBox.Text = prescription.info;
        }
    }
}
