using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class MedicineTableUserControl : UserControl
    {
        ObservableCollection<Medicine> medicineList;
        public MedicineTableUserControl()
        {
            InitializeComponent();
            RefreshTable();
        }
        public void RefreshTable()
        {
            medicineList = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
            medicineTable.ItemsSource = null;
            medicineTable.ItemsSource = medicineList;
        }
        private void medicineTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
