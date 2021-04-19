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
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.Manager
{
    /// <summary>
    /// Interaction logic for AddEquipmentToRoomWindow.xaml
    /// </summary>
    public partial class AddEquipmentToRoomWindow : Window
    {
        private static AddEquipmentToRoomWindow instance = null;
        private ObservableCollection<Equipment> equipmentList;

        public static AddEquipmentToRoomWindow getInstance(int option)
        {
            if (instance == null)
                instance = new AddEquipmentToRoomWindow(option);
            return instance;
        }
        private AddEquipmentToRoomWindow(int option)
        {
            InitializeComponent();
            loadEquipment(option);
        }

        private void loadEquipment(int option)
        {
            if(option == 1)
                equipmentList = new ObservableCollection<Equipment>(EquipmentController.getInstance().getDynamicEquipment());
            else
                equipmentList = new ObservableCollection<Equipment>(EquipmentController.getInstance().getStaticEquipment());

            dynamicEquipmentListBox.ItemsSource = null;
            dynamicEquipmentListBox.ItemsSource = equipmentList;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
