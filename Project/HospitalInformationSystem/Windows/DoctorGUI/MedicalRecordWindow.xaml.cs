using System.Windows;
using Model;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for MedicalRecordWindow.xaml
    /// </summary>
    public partial class MedicalRecordWindow : Window
    {
        Patient _patientToShowMedicalRecord;
        public MedicalRecordWindow(Patient patientToShowMedicalRecord)
        {
            InitializeComponent();
            this._patientToShowMedicalRecord = patientToShowMedicalRecord;
            initPatientsInfo();
        }
        private void initPatientsInfo()
        {
            nameLabel.Content = _patientToShowMedicalRecord.Name + " " + _patientToShowMedicalRecord.Surname;
            bloodTypeLabel.Content = _patientToShowMedicalRecord.Blood.ToString();
            dateOfBirthLabel.Content = _patientToShowMedicalRecord.DateOfBirth.ToString("dd.MM.yyyy.");
            jmbgLeabel.Content = _patientToShowMedicalRecord.Jmbg;
            genderLabel.Content = _patientToShowMedicalRecord.Gender;
            phoneNumberLabel.Content = _patientToShowMedicalRecord.PhoneNumber;
            emailLabel.Content = _patientToShowMedicalRecord.Email;
        }
        private void addAnamnesisButton_Click(object sender, RoutedEventArgs e) => 
            AnamnesisWindow.GetInstance(_patientToShowMedicalRecord.GetMedicalRecord()).ShowDialog();
        private void anamnesisPreviewButton_Click(object sender, RoutedEventArgs e) => 
            AmnesisPreviewWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
        private void addPrescriptionButton_Click(object sender, RoutedEventArgs e) => 
            AddPrescriptionWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
        private void showPrescriptionButton_Click(object sender, RoutedEventArgs e) => 
            DoctorShowPrescription.GetInstance(_patientToShowMedicalRecord.GetMedicalRecord()).ShowDialog();
        private void allergensButton_Click(object sender, RoutedEventArgs e) => 
            DoctorAllergensPreviewWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
        private void referralLetterButton_Click(object sender, RoutedEventArgs e) => 
            ReferralLetterWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
    }
}
