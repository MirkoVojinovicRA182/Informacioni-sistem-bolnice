using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for MedicineTableUserControl.xaml
    /// </summary>
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
