using HospitalInformationSystem.Controller;
using Model;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using HospitalInformationSystem.Model;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {

        private static ManagerMainWindow instance;

        public static ManagerMainWindow getInstance()
        {
            if (instance == null)
                instance = new ManagerMainWindow();
            return instance;
        }
        private ManagerMainWindow()
        {
            EquipmentController.getInstance().loadFromFile();
            RoomController.getInstance().loadFromFile();
            MedicineController.GetInstance().LoadFromFile();
            InitializeComponent();
            roomsUserControl.refreshTable();
            equipmentTable.refreshTable();
            medicineTableUserControl.RefreshTable();

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
            Room room = (Room)this.roomsUserControl.allRoomsTable.SelectedItem;
            if (room != null)
            {
                if (room.IsInRenovationState == 1)
                    RenovationMessageWindow.GetInstance().Show();
                else
                    EditRoomWindow.getInstance(room).Show();
            }
            else
                MessageBox.Show("Niste odabrali prostoriju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void deleteRoomMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (roomsUserControl.allRoomsTable.SelectedItem != null)
            {
                RoomController.getInstance().deleteRoom((Room)this.roomsUserControl.allRoomsTable.SelectedItem);
                moveEquipmentInMagacine();
                ManagerMainWindow.getInstance().roomsUserControl.refreshTable();
                MessageBox.Show("Izabrana prostorija je sada obrisana iz sistema.", "Brisanje prostorije", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Niste odabrali prostoriju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void renovationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (roomsUserControl.allRoomsTable.SelectedItem != null)
                RoomRenovationWindow.GetInstance((Room)roomsUserControl.allRoomsTable.SelectedItem).ShowDialog();
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
            else
                MessageBox.Show("Odaberite opremu iz opšteg prikaza opreme!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void deleteEquipmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Equipment selectedEquipment = null;

            if (equipmentTable.equipmentTable.SelectedItem != null)
                selectedEquipment = (Equipment)this.equipmentTable.equipmentTable.SelectedItem;
            else
            {
                MessageBox.Show("Odaberite opremu iz opšteg prikaza opreme!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            RoomController.getInstance().deleteEquipment(selectedEquipment.Id);
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
            else
                MessageBox.Show("Izaberite lek iz tabele!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void removeMedicineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (medicineTableUserControl.medicineTable.SelectedItem != null)
            {
                MedicineController.GetInstance().DeleteMedicine((Medicine)medicineTableUserControl.medicineTable.SelectedItem);
                MedicineController.GetInstance().FindReplacementMedicineAndDeleteThem((Medicine)medicineTableUserControl.medicineTable.SelectedItem);
                medicineTableUserControl.RefreshTable();
                MessageBox.Show("Izabrani lek je sada obrisan iz sistema.", "Brisanje leka", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Izaberite lek iz tabele!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void mainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roomsUserControl.refreshTable();
            equipmentTable.refreshTable();
            medicineTableUserControl.RefreshTable();
        }

        private void staticDynamicTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roomsUserControl.refreshTable();
            equipmentTable.refreshTable();
            medicineTableUserControl.RefreshTable();
        }
        private void moveEquipmentInMagacine()
        {
            Room selectedRoom = (Room)this.roomsUserControl.allRoomsTable.SelectedItem;
            foreach (DictionaryEntry de in selectedRoom.Equipment)
                EquipmentController.getInstance().moveEquipmentInMagacine(de.Key.ToString(), (int)de.Value);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;

            EquipmentController.getInstance().saveInFile();
            RoomController.getInstance().saveInFile();
            MedicineController.GetInstance().SaveInFile();
        }
    }
}
