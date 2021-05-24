using Model;
using System.Windows;

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
            allergensListBox.ItemsSource = patientToViewAllergens.Allergens;
        }
    }
}
