﻿using Model;
using System.Windows;
using System.Windows.Controls;

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
            medicineComboBox.ItemsSource = patientsMedicalRecord.getPrescriptions();
        }

        private void medicineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startDateTextBox.Text = ((Prescription)medicineComboBox.SelectedItem).startTime.ToString("dd.MM.yyyy.");
            endDateTextBox.Text = ((Prescription)medicineComboBox.SelectedItem).endTime.ToString("dd.MM.yyyy.");
            infoTextBox.Text = ((Prescription)medicineComboBox.SelectedItem).info;
        }
    }
}
