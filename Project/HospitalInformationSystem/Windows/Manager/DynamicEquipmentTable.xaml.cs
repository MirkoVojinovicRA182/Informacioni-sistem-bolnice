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
    /// Interaction logic for DynamicEquipmentTable.xaml
    /// </summary>
    public partial class DynamicEquipmentTable : UserControl
    {
        private Equipment selectedRoom = null;
        private ObservableCollection<Equipment> equipmentList;
        public DynamicEquipmentTable()
        {
            InitializeComponent();
            refreshTable();
        }

        public void refreshTable()
        {
            equipmentList = new ObservableCollection<Equipment>(EquipmentController.getInstance().getDynamicEquipment());
            dynamicEquipmentTable.ItemsSource = null;
            dynamicEquipmentTable.ItemsSource = equipmentList;
        }
    }
}
