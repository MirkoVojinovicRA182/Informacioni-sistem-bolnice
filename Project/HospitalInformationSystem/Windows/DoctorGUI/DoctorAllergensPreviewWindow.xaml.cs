using Model;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorAllergensPreviewWindow.xaml
    /// </summary>
    public partial class DoctorAllergensPreviewWindow : Window
    {
        public DoctorAllergensPreviewWindow(Patient patientToViewAllergens)
        {
            InitializeComponent();
            allergensListBox.ItemsSource = patientToViewAllergens.Allergens;
        }
    }
}
