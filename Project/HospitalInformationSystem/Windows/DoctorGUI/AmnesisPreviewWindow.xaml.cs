using Model;
using System.Windows;
using System.Windows.Controls;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for AmnesisPreviewWindow.xaml
    /// </summary>
    public partial class AmnesisPreviewWindow : Window
    {
        private Patient _patientToPreviewAnamnesis;
        public AmnesisPreviewWindow(Patient patientToPreviewAnamnesis)
        {
            InitializeComponent();
            this._patientToPreviewAnamnesis = patientToPreviewAnamnesis;
            FillBoxesWithSelectedAnamnesisInfo();
        }
        private void FillBoxesWithSelectedAnamnesisInfo()
        {
            anamnesisComboBox.ItemsSource = _patientToPreviewAnamnesis.GetMedicalRecord().getAnamneses();
            patientNameLabel.Content = _patientToPreviewAnamnesis.Name + " " + _patientToPreviewAnamnesis.Surname;
        }
        private void anamnesisComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Anamnesis selectedAnamnesisForPreview = (Anamnesis)anamnesisComboBox.SelectedItem;
            anamnesisTextBlock.Text = selectedAnamnesisForPreview.anamnesis;
        }
        private void anamnesisComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Anamnesis anamnesis = (Anamnesis)anamnesisComboBox.SelectedItem;
            anamnesisTextBlock.Text = anamnesis.anamnesis;
        }
    }
}
