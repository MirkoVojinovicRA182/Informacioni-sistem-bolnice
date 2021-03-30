using System.Windows;
using BusinessLogic;
using Model;

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
            //confirmButton.IsEnabled = false;
            //tryToEnableConfirmButton();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            NewRoomWindow newRoomWindow = new NewRoomWindow();
            AllRoomsWindow allRoomsWindow = new AllRoomsWindow();
            OneRoomWindow oneRoomWindow = new OneRoomWindow();
            DeleteOneRoomWindow deleteOneRoomWindow = new DeleteOneRoomWindow();

            if (RoomDataBase.getInstance().GetRoom().Count != 0)
            {
                if ((bool)allRoomsRadioButton.IsChecked)
                    allRoomsWindow.Show();
                else if ((bool)oneRoomRadioButton.IsChecked)
                    oneRoomWindow.Show();
                else if ((bool)deleteAllRoomsRadioButton.IsChecked)
                {
                    deleteAllRooms();
                    MessageBox.Show("Sve prostorije su sada obrisane iz sistema.", "Operacija brisanja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if ((bool)deleteOneRoomRadioButton.IsChecked)
                    deleteOneRoomWindow.Show();
            }
            else if ((bool)newRadioButton.IsChecked)
                newRoomWindow.Show();
            else
                MessageBox.Show("Ne možete pristupiti odabranoj funkcionalnosti jer u sistemu ne postoji nijedna prostorija.", "Prazna baza", MessageBoxButton.OK, MessageBoxImage.Warning);

              

        }

        private void tryToEnableConfirmButton()
        {
            if ((bool)allRoomsRadioButton.IsChecked || (bool)oneRoomRadioButton.IsChecked || (bool)deleteOneRoomRadioButton.IsChecked || (bool)deleteAllRoomsRadioButton.IsChecked || (bool)newRadioButton.IsChecked)
                confirmButton.IsEnabled = true;
        }

        private void deleteAllRooms()
        {
            RoomManagement management = new RoomManagement();

            management.DeleteAllRooms();
        }

      

       
    }
}
