using System.Windows;

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

            if ((bool)newRadioButton.IsChecked)
                newRoomWindow.Show();
            else if ((bool)allRoomsRadioButton.IsChecked)
                allRoomsWindow.Show();

        }
    }
}
