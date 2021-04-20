using System.Windows;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for MedicalRecordWindow.xaml
    /// </summary>
    public partial class MedicalRecordWindow : Window
    {

        Model.Patient patient;
        public MedicalRecordWindow(Model.Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            initInfo();
        }

        private void initInfo()
        {
            nameLabel.Content = patient.Name + " " + patient.Surname;
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
            AddPrescriptionWindow addPrescriptionWindow = new AddPrescriptionWindow(patient.GetMedicalRecord());

            addPrescriptionWindow.ShowDialog();
        }

        private void showPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorShowPrescription doctorShowPrescription = new DoctorShowPrescription(patient.GetMedicalRecord());

            doctorShowPrescription.ShowDialog();
        }
    }
}
