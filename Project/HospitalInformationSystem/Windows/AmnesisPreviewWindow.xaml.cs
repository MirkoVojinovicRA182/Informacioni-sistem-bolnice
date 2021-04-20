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
    /// Interaction logic for AmnesisPreviewWindow.xaml
    /// </summary>
    public partial class AmnesisPreviewWindow : Window
    {

        private Patient patient;
        public AmnesisPreviewWindow(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            initData();
        }

        private void initData()
        {
            anamnesisComboBox.ItemsSource = patient.GetMedicalRecord().getAnamneses();
            patientNameLabel.Content = patient.Name + " " + patient.Surname;
            //anamnesisTextBlock.Text = patient.GetMedicalRecord().getAnamneses()[0].getAnamnesis();
        }

        private void anamnesisComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Anamnesis anamnesis = (Anamnesis)anamnesisComboBox.SelectedItem;
            anamnesisTextBlock.Text = anamnesis.anamnesis;
        }

        private void anamnesisComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Anamnesis anamnesis = (Anamnesis)anamnesisComboBox.SelectedItem;
            anamnesisTextBlock.Text = anamnesis.anamnesis;
        }
    }
}
