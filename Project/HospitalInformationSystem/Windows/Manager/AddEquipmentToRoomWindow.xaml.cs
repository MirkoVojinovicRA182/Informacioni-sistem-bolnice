using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Windows.Manager.Help;
using Model;
using System;
using System.Collections;
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
        private Equipment selectedEquipment;
        private int quantity;
        private string window;

        public static AddEquipmentToRoomWindow getInstance(string equipment, string window)
        {
            if (instance == null)
                instance = new AddEquipmentToRoomWindow(equipment, window);
            return instance;
        }
        private AddEquipmentToRoomWindow(string equipment, string window)
        {
            InitializeComponent();
            loadEquipment(equipment);
            this.window = window;
        }

        private void loadEquipment(string equipment)
        {
            if(string.Equals(equipment, "staticka"))
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

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            selectedEquipment = (Equipment)dynamicEquipmentListBox.SelectedItem;
            quantity = int.Parse(quantityTextBox.Text);

            if (string.Equals(window, "newRoom"))
                NewRoomWindow.getInstance().addDynamicEquipment(selectedEquipment.Id, quantity);
            else
                EditRoomWindow.getInstance((Room)SelectRoomWindow.getInstance(1).roomsComboBox.SelectedItem).addDynamicEquipment(selectedEquipment.Id, quantity);

            instance = null;
        }

        public Equipment getEquipment()
        {
            return this.selectedEquipment;
        }

        public int getQuantity()
        {
            return this.quantity;
        }
    }
}
