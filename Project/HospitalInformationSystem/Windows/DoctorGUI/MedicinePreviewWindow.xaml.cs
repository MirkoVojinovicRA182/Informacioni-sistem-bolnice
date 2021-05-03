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
        private static MedicinePreviewWindow instance = null;

        public static MedicinePreviewWindow GetInstance()
        {
            if (instance == null)
                instance = new MedicinePreviewWindow();
            return instance;
        }
        private MedicinePreviewWindow()
        {
            InitializeComponent();
            medicineTable.DataContext = MedicineController.GetInstance().GetAllMedicines();
            RefreshTable();
        }

        public void RefreshTable()
        {
            medicineTable.ItemsSource = null;
            medicineTable.ItemsSource = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
        }

        private void previewMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            MedicineInformationPreview medicineInformationPreview = MedicineInformationPreview.GetInstance((Medicine)medicineTable.SelectedItem);
            medicineInformationPreview.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
