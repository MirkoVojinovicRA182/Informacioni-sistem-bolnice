using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public void RefreshTable()
        {
            medicineTable.ItemsSource = null;
            medicineTable.ItemsSource = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
        }
        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                if (medicineTable.SelectedIndex >= 0)
                    MedicineInformationPreview.GetInstance((Medicine)medicineTable.SelectedItem).ShowDialog();
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void medicineTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
