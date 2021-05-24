using HospitalInformationSystem.Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for EquipmentTable.xaml
    /// </summary>
    public partial class GeneralEquipmentViewUserControl : UserControl
    {
        private Equipment selectedEquipment = null;
        private ObservableCollection<Equipment> equipmentList;
        public GeneralEquipmentViewUserControl()
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
