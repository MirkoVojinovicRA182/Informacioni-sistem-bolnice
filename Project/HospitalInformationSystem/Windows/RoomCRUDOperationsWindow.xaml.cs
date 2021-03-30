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

using Model;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for RoomCRUDOperationsWindow.xaml
    /// </summary>
    public partial class RoomCRUDOperationsWindow : Window
    {
        Room selectedRoom;
        private ObservableCollection<Room> roomList;
        public RoomCRUDOperationsWindow()
        {
            InitializeComponent();

            refreshTable();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            NewRoomWindow newRoomWindow = new NewRoomWindow();

            newRoomWindow.ShowDialog();

            refreshTable();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            selectedRoom = (Room)allRoomsTable.SelectedItem;
            EditRoomWindow editRoomWindow = new EditRoomWindow(selectedRoom);

            editRoomWindow.ShowDialog();

            refreshTable();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            selectedRoom = (Room)allRoomsTable.SelectedItem;

            RoomDataBase.getInstance().removeRoom(selectedRoom);

            refreshTable();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void refreshTable()
        {
            roomList = new ObservableCollection<Room>(RoomDataBase.getInstance().getRooms());
            allRoomsTable.ItemsSource = null;
            allRoomsTable.ItemsSource = roomList;
        }

       

    }
}
