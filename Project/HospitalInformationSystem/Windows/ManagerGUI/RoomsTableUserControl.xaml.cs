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

        private void findByNameTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (findByNameTextBox.Text.Equals(""))
                allRoomsTable.ItemsSource = new ObservableCollection<Room>(RoomController.GetInstance().GetRooms());
            else
                FindMedicine();
        }
        private void FindMedicine()
        {
            ObservableCollection<Room> foundedMedicines = new ObservableCollection<Room>();
            ObservableCollection<Room> all = new ObservableCollection<Room>(RoomController.GetInstance().GetRooms());
            foreach (Room medicine in all)
            {
                if (medicine.Name.StartsWith(findByNameTextBox.Text))
                    foundedMedicines.Add(medicine);
            }
            allRoomsTable.ItemsSource = null;
            allRoomsTable.ItemsSource = foundedMedicines;
        }
    }
}
