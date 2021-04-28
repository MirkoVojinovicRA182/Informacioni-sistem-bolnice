using System.Windows;
using Model;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for MedicalRecordWindow.xaml
    /// </summary>
    public partial class MedicalRecordWindow : Window
    {

        Patient patient;
        public MedicalRecordWindow(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            initInfo();
        }

        private void initInfo()
        {
            nameLabel.Content = patient.Name + " " + patient.Surname;
            bloodTypeLabel.Content = patient.Blood.ToString();
            dateOfBirthLabel.Content = patient.DateOfBirth.ToString("dd.MM.yyyy.");
            jmbgLeabel.Content = patient.Jmbg;
            genderLabel.Content = patient.Gender;
            phoneNumberLabel.Content = patient.PhoneNumber;
            emailLabel.Content = patient.Email;
        }

        private void addAnamnesisButton_Click(object sender, RoutedEventArgs e)
        {
            AnamnesisWindow anamnesisWindow = new AnamnesisWindow(patient.GetMedicalRecord());

            anamnesisWindow.ShowDialog();
        }

        private void anamnesisPreviewButton_Click(object sender, RoutedEventArgs e)
        {
            AmnesisPreviewWindow amnesisPreviewWindow = new AmnesisPreviewWindow(patient);

            amnesisPreviewWindow.ShowDialog();
        }

        private void addPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            AddPrescriptionWindow addPrescriptionWindow = new AddPrescriptionWindow(patient);

            addPrescriptionWindow.ShowDialog();
        }

        private void showPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorShowPrescription doctorShowPrescription = new DoctorShowPrescription(patient.GetMedicalRecord());

            doctorShowPrescription.ShowDialog();
        }
    }
}
