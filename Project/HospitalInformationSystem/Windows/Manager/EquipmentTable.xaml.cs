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

namespace HospitalInformationSystem.Windows.Manager
{
    /// <summary>
    /// Interaction logic for EquipmentTable.xaml
    /// </summary>
    public partial class EquipmentTable : UserControl
    {
        private Equipment selectedEquipment = null;
        private ObservableCollection<Equipment> equipmentList;
        public EquipmentTable()
        {
            InitializeComponent();
            refreshTable();
        }

        public void refreshTable()
        {
            equipmentList = new ObservableCollection<Equipment>(EquipmentController.getInstance().getStaticEquipment());
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
        }
    }
}
