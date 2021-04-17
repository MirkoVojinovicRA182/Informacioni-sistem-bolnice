﻿using System;
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

using Model;
using BusinessLogic;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for RoomCRUDOperationsWindow.xaml
    /// </summary>
    public partial class RoomCRUDOperationsWindow : System.Windows.Controls.UserControl
    {
        private Room selectedRoom = null;
        private ObservableCollection<Room> roomList;
        private RoomManagement roomManagement = new RoomManagement();

        public RoomCRUDOperationsWindow()
        {
            InitializeComponent();

            refreshTable();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            NewRoomWindow.getInstance().ShowDialog();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            selectedRoom = (Room)allRoomsTable.SelectedItem;
            if (selectedRoom != null)
                EditRoomWindow.getInstance(selectedRoom).ShowDialog();
            else
                MessageBox.Show("Niste izabrali prostoriju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            selectedRoom = (Room)allRoomsTable.SelectedItem;

            if (selectedRoom != null)
                roomManagement.deleteRoom(selectedRoom);
            else
                MessageBox.Show("Niste izabrali prostoriju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            refreshTable();
        }

        public void refreshTable()
        {
            roomList = new ObservableCollection<Room>(RoomDataBase.getInstance().getRooms());
            allRoomsTable.ItemsSource = null;
            allRoomsTable.ItemsSource = roomList;

            if(RoomDataBase.getInstance().getRooms().Count != 0)
            {
                changeButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
            }
            else
            {
                changeButton.IsEnabled = false;
                deleteButton.IsEnabled = false;
            }
        }

    }
}
