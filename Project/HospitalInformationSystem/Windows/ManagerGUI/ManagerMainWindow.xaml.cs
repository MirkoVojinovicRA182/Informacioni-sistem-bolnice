using HospitalInformationSystem.Controller;
using Model;
using System.Windows;
using System.Windows.Controls;
using HospitalInformationSystem.Model;
using MahApps.Metro.Controls;
using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : MetroWindow
    {
        Room room;
        private static ManagerMainWindow instance;

        public static ManagerMainWindow getInstance()
        {
            if (instance == null)
                instance = new ManagerMainWindow();
            return instance;
        }
        private ManagerMainWindow()
        {
            InitializeComponent();
            RefreshTables();
            if (MedicineCommentsExists())
                MedicineCommentNotificationWindow.GetInstance().ShowDialog();

        }
        private void SetTheme()
        {
            ThemeManager.Current.ChangeTheme(this, "Light.Green");
            lightTheme.IsChecked = true;

        }
        private void RefreshTables()
        {
            roomsUserControl.refreshTable();
            equipmentTable.refreshTable();
            detailEquipmentTable.LoadAllUserControlComponents();
            medicineTableUserControl.RefreshTable();
        }
        private bool MedicineCommentsExists()
        {
            return MedicineController.GetInstance().MedicineCommentExists();
        }
        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            instance = null;
        }
        private void newRoomMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewRoomWindow.getInstance().Show();
        }
        private void editRoomMenuItem_Click(object sender, RoutedEventArgs e)
        {
            room = (Room)this.roomsUserControl.allRoomsTable.SelectedItem;
            if (room != null)
            {
                if (room.RoomRenovationState.ActivityStatus)
                    RenovationMessageWindow.GetInstance().Show();
                else
                    EditRoomWindow.getInstance(room).Show();
            }
        }
        private void deleteRoomMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (roomsUserControl.allRoomsTable.SelectedItem != null)
            {
                room = (Room)this.roomsUserControl.allRoomsTable.SelectedItem;
                if (string.Equals(room.Name, "Magacin"))
                    return;
                if (!room.RoomRenovationState.ActivityStatus)
                {
                    RoomController.GetInstance().DeleteRoom((Room)this.roomsUserControl.allRoomsTable.SelectedItem);
                    ManagerMainWindow.getInstance().roomsUserControl.refreshTable();
                    MessageBox.Show("Izabrana prostorija je sada obrisana iz sistema.", "Brisanje prostorije", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    RenovationMessageWindow.GetInstance().Show();
            }
            else
                MessageBox.Show("Niste odabrali prostoriju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void renovationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = (Room)roomsUserControl.allRoomsTable.SelectedItem;
            if (roomsUserControl.allRoomsTable.SelectedItem != null)
            {
                if (selectedRoom.RoomRenovationState.ActivityStatus)
                    RenovationMessageWindow.GetInstance().ShowDialog();
                else
                    RoomRenovationWindow.GetInstance(selectedRoom).ShowDialog();
            }
            else
                MessageBox.Show("Izaberite prostoriju iz tabele!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
        private void newEquipmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewEquipment.getInstance().Show();
        }
        private void editEquipmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (equipmentTable.equipmentTable.SelectedItem != null)
                EditEquipment.getInstance((Equipment)this.equipmentTable.equipmentTable.SelectedItem).Show();
        }
        private void deleteEquipmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Equipment selectedEquipment = null;

            if (equipmentTable.equipmentTable.SelectedItem != null)
                selectedEquipment = (Equipment)this.equipmentTable.equipmentTable.SelectedItem;
            else
            {
                return;
            }
            EquipmentController.getInstance().deleteEquipment(selectedEquipment);
            ManagerMainWindow.getInstance().equipmentTable.refreshTable();
            MessageBox.Show("Izabrana oprema je sada obrisana iz sistema.", "Brisanje opreme", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void newMedicineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewMedicineWindow.GetInstance().Show();
        }
        private void editMedicineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (medicineTableUserControl.medicineTable.SelectedItem != null)
            {
                EditMedicineWindow.GetInstance((Medicine)medicineTableUserControl.medicineTable.SelectedItem).ShowDialog();
                medicineTableUserControl.RefreshTable();
            }
        }
        private void removeMedicineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (medicineTableUserControl.medicineTable.SelectedItem != null)
            {
                MedicineController.GetInstance().DeleteMedicine((Medicine)medicineTableUserControl.medicineTable.SelectedItem);
                MedicineController.GetInstance().DeleteReplacementMedicine((Medicine)medicineTableUserControl.medicineTable.SelectedItem);
                medicineTableUserControl.RefreshTable();
                MessageBox.Show("Izabrani lek je sada obrisan iz sistema.", "Brisanje leka", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void medicineComments_Click(object sender, RoutedEventArgs e)
        {
            MedicineWithCommentPreview.GetInstance().Show();
        }
        private void mainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshTables();
        }
        private void staticDynamicTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshTables();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
            MainWindow.Serialize();
            
        }

        private void lightTheme_Checked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Light.Green");
            darkTheme.IsChecked = false;
        }

        private void darkTheme_Checked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Dark.Green");
            lightTheme.IsChecked = false;
        }
    }
}
