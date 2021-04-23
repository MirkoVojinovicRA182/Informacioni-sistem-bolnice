using HospitalInformationSystem.Service;
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
    /// Interaction logic for StaticEquipmentDeploymentWindow.xaml
    /// </summary>
    public partial class StaticEquipmentDeploymentWindow : Window
    {
        private static StaticEquipmentDeploymentWindow instance = null;
        private Room currentRoom;
        private Room nextRoom;
        private int quantity;
        private ObservableCollection<Room> roomList;
        private int quantityOfSelectedEquipment;
        private string idOfSelectedEquipment;
        public static StaticEquipmentDeploymentWindow getInstance(Room room, int value, string key)
        {
            if (instance == null)
                instance = new StaticEquipmentDeploymentWindow(room, value, key);
            return instance;
        }
        private StaticEquipmentDeploymentWindow(Room room, int value, string key)
        {
            InitializeComponent();
            currentRoom = room;
            quantityOfSelectedEquipment = value;
            idOfSelectedEquipment = key;
            fillControls();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            nextRoom = (Room)nextRoomComboBox.SelectedItem;
            quantity = int.Parse(quantityTextBox.Text);

            RoomService RoomService = new RoomService();

            //brisanje opreme iz trenutne prostorije
            RoomService.changeStaticEquipmentState(currentRoom, quantityOfSelectedEquipment, quantity, idOfSelectedEquipment);


            //dodavanje opreme u zeljenu prostoriju
            RoomService.moveStaticEqToNextRoom(nextRoom, quantity, idOfSelectedEquipment);

            //osvezavanje staticke opreme izabrane prostorije
            EditRoomWindow.getInstance(currentRoom).loadRoom();

            SuccessMovingWindow.getInstance(quantityOfSelectedEquipment, quantityOfSelectedEquipment - quantity).Show();

            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void fillControls()
        {
            currentRoomTextBlock.Text = currentRoom.Name;
            loadComboBox();
        }

        private void loadComboBox()
        {
            RoomService RoomService = new RoomService();
            roomList = new ObservableCollection<Room>(RoomService.getRooms());

            //brisanje trenutne prostorije iz liste svih potencijalnih prostorija
            roomList.Remove(currentRoom);

            nextRoomComboBox.ItemsSource = null;
            nextRoomComboBox.ItemsSource = roomList;
        }
    }
}
