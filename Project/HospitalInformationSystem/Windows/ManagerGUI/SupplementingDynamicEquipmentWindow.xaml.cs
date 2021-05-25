using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class SupplementingDynamicEquipmentWindow : Window
    {
        Room selectedRoom;
        string idOfSelectedEquipment;
        private static SupplementingDynamicEquipmentWindow instance;
        public static SupplementingDynamicEquipmentWindow GetInstance(Room selectedRoom, string idOfSelectedEquipment)
        {
            if (instance == null)
                instance = new SupplementingDynamicEquipmentWindow(selectedRoom, idOfSelectedEquipment);
            return instance;
        }
        private SupplementingDynamicEquipmentWindow(Room selectedRoom, string idOfSelectedEquipment)
        {
            InitializeComponent();
            this.selectedRoom = selectedRoom;
            this.idOfSelectedEquipment = idOfSelectedEquipment;
        }
        private void SupplyRoomDynamicEquipment(Room selectedRoom, string idOfSelectedEquipment)
        {
            int currentQuantity = (int)selectedRoom.EquipmentInRoom.Equipment[idOfSelectedEquipment];
            selectedRoom.EquipmentInRoom.Equipment[idOfSelectedEquipment] = currentQuantity + int.Parse(quantityTextBox.Text); 
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTheEnteredQuantity())
            {
                SupplyRoomDynamicEquipment(selectedRoom, idOfSelectedEquipment);
                RoomController.GetInstance().GetMagacine().EquipmentInRoom.ReduceEquipmentQuantity(idOfSelectedEquipment, int.Parse(quantityTextBox.Text));
                EditRoomWindow.getInstance(selectedRoom).LoadRoomEquipment();
                EditRoomWindow.getInstance(selectedRoom).RefreshDynamicEquipmentListBox();
                this.Close();
            }
            else
                MessageBox.Show("Pogrešan unos količine!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
        private bool CheckTheEnteredQuantity()
        {
            int enteredQuantity = int.TryParse(quantityTextBox.Text, out enteredQuantity) ? enteredQuantity : 0;
            return enteredQuantity > 0 && enteredQuantity <= (int)RoomController.GetInstance().GetMagacine().EquipmentInRoom.Equipment[idOfSelectedEquipment];
        }
    }
}
