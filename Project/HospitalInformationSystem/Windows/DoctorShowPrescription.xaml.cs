using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows
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
