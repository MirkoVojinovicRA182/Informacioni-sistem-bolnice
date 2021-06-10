using System.Windows;
using System.Windows.Input;
using Model;
using static HospitalInformationSystem.Utility.Constants;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for MedicalRecordWindow.xaml
    /// </summary>
    public partial class MedicalRecordWindow : Window
    {
        private Patient _patientToShowMedicalRecord;
        private static MedicalRecordWindow instance = null;
        public static MedicalRecordWindow GetInstance(Patient patientToShowMedicalRecord)
        {
            if (instance == null)
                instance = new MedicalRecordWindow(patientToShowMedicalRecord);
            return instance;
        }
        private MedicalRecordWindow(Patient patientToShowMedicalRecord)
        {
            InitializeComponent();
            this._patientToShowMedicalRecord = patientToShowMedicalRecord;
            initPatientsInfo();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void initPatientsInfo()
        {
            if (_patientToShowMedicalRecord.MedicalRecord.getId() != null)
                medicalRecordId.Content = _patientToShowMedicalRecord.GetMedicalRecordId;
            nameLabel.Content = _patientToShowMedicalRecord.Name + " " + _patientToShowMedicalRecord.Surname;
            bloodTypeLabel.Content = _patientToShowMedicalRecord.Blood.ToString();
            dateOfBirthLabel.Content = _patientToShowMedicalRecord.DateOfBirth.ToString(DATE_TEMPLATE);
            jmbgLeabel.Content = _patientToShowMedicalRecord.Jmbg;
            genderLabel.Content = _patientToShowMedicalRecord.Gender;
            phoneNumberLabel.Content = _patientToShowMedicalRecord.PhoneNumber;
            emailLabel.Content = _patientToShowMedicalRecord.Email;
        }
        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.A))
                AnamnesisWindow.GetInstance(_patientToShowMedicalRecord.MedicalRecord).ShowDialog();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.P))
                AmnesisPreviewWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.R))
                AddPrescriptionWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.W))
                DoctorShowPrescription.GetInstance(_patientToShowMedicalRecord.MedicalRecord).ShowDialog();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.T))
                DoctorAllergensPreviewWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.U))
                ReferralLetterWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.Q))
                this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => instance = null;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
