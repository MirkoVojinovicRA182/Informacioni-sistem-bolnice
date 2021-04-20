using BusinessLogic;
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
    /// Interaction logic for StaticEquipmentDeploymentWindow.xaml
    /// </summary>
    public partial class StaticEquipmentDeploymentWindow : Window
    {
        private static StaticEquipmentDeploymentWindow instance = null;
        private Room currentRoom;
        private ObservableCollection<Room> roomList;
        public static StaticEquipmentDeploymentWindow getInstance(Room room)
        {
            if (instance == null)
                instance = new StaticEquipmentDeploymentWindow(room);
            return instance;
        }
        private StaticEquipmentDeploymentWindow(Room room)
        {
            InitializeComponent();
            currentRoom = room;
            fillControls();
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
            RoomManagement roomManagement = new RoomManagement();
            roomList = new ObservableCollection<Room>(roomManagement.getRooms());

            //brisanje trenutne prostorije iz liste svih potencijalnih prostorija
            roomList.Remove(currentRoom);

            nextRoomComboBox.ItemsSource = null;
            nextRoomComboBox.ItemsSource = roomList;
        }
    }
}
