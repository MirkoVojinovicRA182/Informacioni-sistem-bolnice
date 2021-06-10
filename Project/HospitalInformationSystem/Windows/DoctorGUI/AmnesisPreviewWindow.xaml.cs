using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for AmnesisPreviewWindow.xaml
    /// </summary>
    public partial class AmnesisPreviewWindow : Window
    {
        private Patient _patientToPreviewAnamnesis;
        private static AmnesisPreviewWindow instance = null;
        public static AmnesisPreviewWindow GetInstance(Patient patientToPreviewAnamnesis)
        {
            if (instance == null)
                instance = new AmnesisPreviewWindow(patientToPreviewAnamnesis);
            return instance;
        }
        private AmnesisPreviewWindow(Patient patientToPreviewAnamnesis)
        {
            InitializeComponent();
            this._patientToPreviewAnamnesis = patientToPreviewAnamnesis;
            FillBoxesWithSelectedAnamnesisInfo();
        }
        private void FillBoxesWithSelectedAnamnesisInfo()
        {
            anamnesisListBox.ItemsSource = _patientToPreviewAnamnesis.MedicalRecord.getAnamneses();
            patientNameLabel.Content = _patientToPreviewAnamnesis.Name + " " + _patientToPreviewAnamnesis.Surname;
        }
        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void anamnesisListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Anamnesis anamnesis = (Anamnesis)anamnesisListBox.SelectedItem;
            anamnesisTextBlock.Text = anamnesis.anamnesis;
        }
    }
}
