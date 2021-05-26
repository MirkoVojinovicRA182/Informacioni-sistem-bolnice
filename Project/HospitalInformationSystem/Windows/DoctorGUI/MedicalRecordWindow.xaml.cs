﻿using System.Windows;
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
        }
        private void initPatientsInfo()
        {
            nameLabel.Content = _patientToShowMedicalRecord.Name + " " + _patientToShowMedicalRecord.Surname;
            bloodTypeLabel.Content = _patientToShowMedicalRecord.Blood.ToString();
            dateOfBirthLabel.Content = _patientToShowMedicalRecord.DateOfBirth.ToString(DATE_TEMPLATE);
            jmbgLeabel.Content = _patientToShowMedicalRecord.Jmbg;
            genderLabel.Content = _patientToShowMedicalRecord.Gender;
            phoneNumberLabel.Content = _patientToShowMedicalRecord.PhoneNumber;
            emailLabel.Content = _patientToShowMedicalRecord.Email;
        }
        private void addAnamnesisButton_Click(object sender, RoutedEventArgs e) => 
            AnamnesisWindow.GetInstance(_patientToShowMedicalRecord.MedicalRecord).ShowDialog();
        private void anamnesisPreviewButton_Click(object sender, RoutedEventArgs e) => 
            AmnesisPreviewWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
        private void addPrescriptionButton_Click(object sender, RoutedEventArgs e) => 
            AddPrescriptionWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
        private void showPrescriptionButton_Click(object sender, RoutedEventArgs e) => 
            DoctorShowPrescription.GetInstance(_patientToShowMedicalRecord.MedicalRecord).ShowDialog();
        private void allergensButton_Click(object sender, RoutedEventArgs e) => 
            DoctorAllergensPreviewWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
        private void referralLetterButton_Click(object sender, RoutedEventArgs e) => 
            ReferralLetterWindow.GetInstance(_patientToShowMedicalRecord).ShowDialog();
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => instance = null;
    }
}
