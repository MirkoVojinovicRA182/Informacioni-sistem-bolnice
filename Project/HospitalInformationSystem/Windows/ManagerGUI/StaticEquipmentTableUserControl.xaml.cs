using HospitalInformationSystem.Controller;
using Model;
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
    /// Interaction logic for EquipmentTable.xaml
    /// </summary>
    public partial class StaticEquipmentTableUserControl : UserControl
    {
        private Equipment selectedEquipment = null;
        private ObservableCollection<Equipment> equipmentList;
        public StaticEquipmentTableUserControl()
        {
            InitializeComponent();
            refreshTable();
        }
        public void refreshTable()
        {
            equipmentList = new ObservableCollection<Equipment>(EquipmentController.getInstance().getEquipment());
            equipmentTable.ItemsSource = null;
            equipmentTable.ItemsSource = equipmentList;
        }
        public Equipment getSelectedEquipment()
        {
            return this.selectedEquipment;
        }

        private void equipmentTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selectedEquipment = (Equipment)equipmentTable.SelectedItem;
            e.Handled = true;
        }
    }
}
