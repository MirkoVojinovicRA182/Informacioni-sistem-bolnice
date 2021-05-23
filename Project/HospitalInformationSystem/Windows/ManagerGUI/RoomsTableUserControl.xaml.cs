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
    /// Interaction logic for RoomsTableUserControl.xaml
    /// </summary>
    public partial class RoomsTableUserControl : UserControl
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
