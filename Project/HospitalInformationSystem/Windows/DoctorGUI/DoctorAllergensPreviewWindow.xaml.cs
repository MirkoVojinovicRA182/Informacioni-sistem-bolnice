using Model;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorAllergensPreviewWindow.xaml
    /// </summary>
    public partial class DoctorAllergensPreviewWindow : Window
    {
        private static DoctorAllergensPreviewWindow instance = null;
        public static DoctorAllergensPreviewWindow GetInstance(Patient patientToViewAllAllergens)
        {
            if (instance == null)
                instance = new DoctorAllergensPreviewWindow(patientToViewAllAllergens);
            return instance;
        }
        private DoctorAllergensPreviewWindow(Patient patientToViewAllergens)
        {
            InitializeComponent();
            allergensListBox.ItemsSource = patientToViewAllergens.MedicalRecord.AllergensList;
        }
        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
        }
        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void allergensListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
