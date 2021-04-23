using Service;
using HospitalInformationSystem.Controller;
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

namespace HospitalInformationSystem.Windows.Manager.Help
{
    /// <summary>
    /// Interaction logic for SelectRoomWindow.xaml
    /// </summary>
    public partial class SelectRoomWindow : Window
    {
        private ObservableCollection<Room> roomList;
        private static SelectRoomWindow instance = null;

        int selection;

        public static SelectRoomWindow getInstance(int selection)
        {
            if (instance == null)
                instance = new SelectRoomWindow(selection);
            return instance;
        }
        private SelectRoomWindow(int selection)
        {
            InitializeComponent();

            this.selection = selection;

            roomList = new ObservableCollection<Room>(RoomDataBase.getInstance().getRooms());

            roomsComboBox.ItemsSource = roomList;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            RoomService RoomService = new RoomService();

            if (selection == 2)
            {

                RoomService.deleteRoom((Room)roomsComboBox.SelectedItem);
                moveEquipmentInMagacine();
                ManagerMainWindow.getInstance().roomsTable.refreshTable();
                MessageBox.Show("Izabrana prostorija je sada obrisana iz sistema.", "Brisanje prostorije", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                EditRoomWindow.getInstance((Room)roomsComboBox.SelectedItem).Show();
            }

            this.Close();
        }

        private void moveEquipmentInMagacine()
        {
            Room selectedRoom = (Room)roomsComboBox.SelectedItem;
            foreach (DictionaryEntry de in selectedRoom.Equipment)
                EquipmentController.getInstance().moveEquipmentInMagacine(de.Key.ToString(), (int)de.Value);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
