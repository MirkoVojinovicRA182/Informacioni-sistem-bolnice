using HospitalInformationSystem.Model;
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
    }
}
