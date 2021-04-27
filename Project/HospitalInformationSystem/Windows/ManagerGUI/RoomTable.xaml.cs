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
using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Service;
using Model;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for RoomTable.xaml
    /// </summary>
    public partial class RoomTable : UserControl
    {
        private Room selectedRoom = null;
        private ObservableCollection<Room> roomList;
        private RoomService RoomService = new RoomService();

        public RoomTable()
        {
            InitializeComponent();

            refreshTable();
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
                RoomService.deleteRoom(selectedRoom);
            else
                MessageBox.Show("Niste izabrali prostoriju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            refreshTable();
        }

        public void refreshTable()
        {
            roomList = new ObservableCollection<Room>(RoomController.getInstance().getRooms());
            allRoomsTable.ItemsSource = null;
            allRoomsTable.ItemsSource = roomList;

            allRoomsTable.AreRowDetailsFrozen = false;

            if (RoomController.getInstance().getRooms().Count != 0)
            {
                //changeButton.IsEnabled = true;
                //deleteButton.IsEnabled = true;
            }
            else
            {
                //changeButton.IsEnabled = false;
                //deleteButton.IsEnabled = false;
            }
        }

        private void allRoomsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
