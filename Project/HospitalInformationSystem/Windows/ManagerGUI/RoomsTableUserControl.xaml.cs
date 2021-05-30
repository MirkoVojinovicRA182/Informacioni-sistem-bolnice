using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using HospitalInformationSystem.Controller;
using MahApps.Metro.Controls;
using Model;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for RoomsTableUserControl.xaml
    /// </summary>
    public partial class RoomsTableUserControl : MetroContentControl
    {
        private ObservableCollection<Room> roomList;
        public RoomsTableUserControl()
        {
            InitializeComponent();
            refreshTable();
        }
        public void refreshTable()
        {
            roomList = new ObservableCollection<Room>(RoomController.GetInstance().GetRooms());
            allRoomsTable.ItemsSource = null;
            allRoomsTable.ItemsSource = roomList;
        }
        private void allRoomsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }
        private void allRoomsTable_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            refreshTable();
            e.Handled = true;
        }
    }
}
