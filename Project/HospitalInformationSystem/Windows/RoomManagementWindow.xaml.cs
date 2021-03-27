using System.Windows;
using BusinessLogic;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for RoomManagementWindow.xaml
    /// </summary>
    public partial class RoomManagementWindow : Window
    {
        public RoomManagementWindow()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            NewRoomWindow newRoomWindow = new NewRoomWindow();
            AllRoomsWindow allRoomsWindow = new AllRoomsWindow();
            OneRoomWindow oneRoomWindow = new OneRoomWindow();

            if ((bool)newRadioButton.IsChecked)
                newRoomWindow.Show();
            else if ((bool)allRoomsRadioButton.IsChecked)
                allRoomsWindow.Show();
            else if ((bool)oneRoomRadioButton.IsChecked)
                oneRoomWindow.Show();
            else if ((bool)deleteAllRoomsRadioButton.IsChecked)
            {
                deleteAllRooms();
                MessageBox.Show("Sve prostorije su sada obrisane iz sistema.", "Operacija brisanja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
              

        }

        private void deleteAllRooms()
        {
            RoomManagement management = new RoomManagement();

            management.DeleteAllRooms();
        }
    }
}
