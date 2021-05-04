using Model;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorAllergensPreviewWindow.xaml
    /// </summary>
    public partial class DoctorAllergensPreviewWindow : Window
    {
        public DoctorAllergensPreviewWindow(MedicalRecord medicalRecord)
        {
            InitializeComponent();
            allergensListBox.DataContext = medicalRecord.AllergensList;
        }
    }
}
