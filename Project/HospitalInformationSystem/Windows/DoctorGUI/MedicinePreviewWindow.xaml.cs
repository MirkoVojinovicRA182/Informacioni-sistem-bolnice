using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for MedicinePreviewWindow.xaml
    /// </summary>
    public partial class MedicinePreviewWindow : Window
    {
        public MedicinePreviewWindow()
        {
            InitializeComponent();
            medicineTable.DataContext = MedicineController.GetInstance().GetAllMedicines();
            refreshTable();
        }

        private void refreshTable()
        {
            medicineTable.ItemsSource = null;
            medicineTable.ItemsSource = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
        }

        private void previewMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            MedicineInformationPreview medicineInformationPreview = new MedicineInformationPreview((Medicine)medicineTable.SelectedItem);
            medicineInformationPreview.ShowDialog();
        }
    }
}
