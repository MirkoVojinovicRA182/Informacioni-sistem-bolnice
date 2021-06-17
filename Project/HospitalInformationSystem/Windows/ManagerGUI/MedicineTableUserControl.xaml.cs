using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class MedicineTableUserControl : MetroContentControl
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

        private void findByNameTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(findByNameTextBox.Text.Equals(""))
                medicineTable.ItemsSource = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
            else
                FindMedicine();
        }

        private void FindMedicine()
        {
            ObservableCollection<Medicine> foundedMedicines = new ObservableCollection<Medicine>();
            ObservableCollection<Medicine> all = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
            foreach(Medicine medicine in all)
            {
                if (medicine.Name.StartsWith(findByNameTextBox.Text))
                    foundedMedicines.Add(medicine);
            }
            medicineTable.ItemsSource = null;
            medicineTable.ItemsSource = foundedMedicines;
        }
    }
}
