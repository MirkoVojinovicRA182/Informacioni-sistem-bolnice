using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Utility;
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

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class AddEquipmentToRoomWindow : Window
    {
        private static AddEquipmentToRoomWindow instance = null;
        private Equipment selectedEquipment;
        private int quantity;
        private string window;
        public static AddEquipmentToRoomWindow getInstance(Hashtable currentRoomEquipment, string equipmentType, string window)
        {
            if (instance == null)
                instance = new AddEquipmentToRoomWindow(currentRoomEquipment, equipmentType, window);
            return instance;
        }
        private AddEquipmentToRoomWindow(Hashtable currentRoomEquipment, string equipmentType, string window)
        {
            InitializeComponent();
            equipmentListBox.ItemsSource = null;
            equipmentListBox.ItemsSource = EquipmentController.getInstance().MakeEquipmentForRoom(equipmentType, currentRoomEquipment);
            this.window = window;
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (equipmentListBox.SelectedItem != null)
            {
                TryAddEquipmentToRoom();
                RefreshListBoxes();
            }
            else
                MessageBox.Show("Niste odabrali opremu!", Constants.WARNING_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void TryAddEquipmentToRoom()
        {
            if (QuantityInputIsOk())
            {
                AddEquipment();
                this.Close();
            }
            else
                MessageBox.Show("Pogrešan unos količine!", Constants.ERROR_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private bool QuantityInputIsOk()
        {
            selectedEquipment = (Equipment)equipmentListBox.SelectedItem;
            quantity = int.TryParse(quantityTextBox.Text, out quantity) ? quantity : 0;
            return quantity > 0 && quantity <= (int)RoomController.GetInstance().GetMagacine().EquipmentInRoom.Equipment[selectedEquipment.Id];
        }
        private void AddEquipment()
        {
            if (window.Equals(Constants.NEW_ROOM_WINDOW))
            {
                NewRoomWindow.getInstance().addEquipmentToRoom(selectedEquipment.Id, quantity);
            }
            else
                EditRoomWindow.getInstance((Room)ManagerMainWindow.getInstance().roomsUserControl.allRoomsTable.SelectedItem).addEquipment(selectedEquipment.Id, quantity);
        }
        private void RefreshListBoxes()
        {
            if (window.Equals(Constants.NEW_ROOM_WINDOW))
            {
                NewRoomWindow.getInstance().refreshDynamicEquipmentListBox();
                NewRoomWindow.getInstance().refreshStaticEquipmentListBox();
            }
            else
            {
                EditRoomWindow.getInstance((Room)ManagerMainWindow.getInstance().roomsUserControl.allRoomsTable.SelectedItem).refreshDynamicEquipmentListBox();
                EditRoomWindow.getInstance((Room)ManagerMainWindow.getInstance().roomsUserControl.allRoomsTable.SelectedItem).refreshStaticEquipmentListBox();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
