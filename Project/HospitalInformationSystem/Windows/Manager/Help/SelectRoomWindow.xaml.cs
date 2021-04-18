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

namespace HospitalInformationSystem.Windows.Manager.Help
{
    /// <summary>
    /// Interaction logic for SelectRoomWindow.xaml
    /// </summary>
    public partial class SelectRoomWindow : Window
    {
        private ObservableCollection<Room> roomList;
        private static SelectRoomWindow instance = null;

        public static SelectRoomWindow getInstance()
        {
            if (instance == null)
                instance = new SelectRoomWindow();
            return instance;
        }
        private SelectRoomWindow()
        {
            InitializeComponent();

            roomList = new ObservableCollection<Room>(RoomDataBase.getInstance().getRooms());

            roomsComboBox.ItemsSource = roomList;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            RoomManagement roomManagement = new RoomManagement();

            roomManagement.deleteRoom((Room)roomsComboBox.SelectedItem);

            MessageBox.Show("Izabrana prostorija je sada obrisana iz sistema.", "Brisanje prostorije", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
