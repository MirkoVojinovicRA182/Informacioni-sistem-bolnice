using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
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
using HospitalInformationSystem.Repository;
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
        private RoomRepository roomRepository;

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
            InitializeComponent();
            roomRepository = new RoomRepository();
            roomsUserControl.refreshTable();
            equipmentTable.refreshTable();

        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            instance = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;

            EquipmentController.getInstance().saveInFile();
            RoomController.getInstance().saveInFile();
        }

        private void newMenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (roomTab.IsSelected)
            {
                NewRoomWindow.getInstance().Show();
            }
            else if (equipmentTab.IsSelected)//tab oprema
            {
                int selectedEquipment;

                if (staticEquipmentTab.IsSelected)
                    selectedEquipment = 1;
                else
                    selectedEquipment = 2;

                NewEquipment.getInstance(selectedEquipment).Show();
            }
        }

        private void mainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roomsUserControl.refreshTable();
            equipmentTable.refreshTable();
            dynamicEquipmentTable.refreshTable();
            detailEquipmentTable.LoadAllUserControlComponents();
        }

        private void staticDynamicTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roomsUserControl.refreshTable();
            equipmentTable.refreshTable();
            dynamicEquipmentTable.refreshTable();
            detailEquipmentTable.LoadAllUserControlComponents();
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (roomTab.IsSelected) //tab prostorije
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
            else if (equipmentTab.IsSelected)//tab oprema
            {
                Equipment selectedEquipment = null;
                if (staticEquipmentTab.IsSelected)
                {
                    if (equipmentTable.equipmentTable.SelectedItem != null)
                        selectedEquipment = (Equipment)this.equipmentTable.equipmentTable.SelectedItem;
                    else
                    {
                        MessageBox.Show("Niste odabrali opremu!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else if (dynamicEquipmentTab.IsSelected)
                {
                    if(dynamicEquipmentTable.dynamicEquipmentTable.SelectedItem != null)
                        selectedEquipment = (Equipment)this.dynamicEquipmentTable.dynamicEquipmentTable.SelectedItem;
                    else
                    {
                        MessageBox.Show("Niste odabrali opremu!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                RoomController.getInstance().deleteEquipment(selectedEquipment.Id);
                EquipmentController.getInstance().deleteEquipment(selectedEquipment);
                ManagerMainWindow.getInstance().equipmentTable.refreshTable();
                ManagerMainWindow.getInstance().dynamicEquipmentTable.refreshTable();
                MessageBox.Show("Izabrana oprema je sada obrisana iz sistema.", "Brisanje opreme", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void moveEquipmentInMagacine()
        {
            Room selectedRoom = (Room)this.roomsUserControl.allRoomsTable.SelectedItem;
            foreach (DictionaryEntry de in selectedRoom.Equipment)
                EquipmentController.getInstance().moveEquipmentInMagacine(de.Key.ToString(), (int)de.Value);
        }


        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (roomTab.IsSelected) //tab prostorije
            {
                if (roomsUserControl.allRoomsTable.SelectedItem != null)
                    EditRoomWindow.getInstance((Room)this.roomsUserControl.allRoomsTable.SelectedItem).Show();
                else
                    MessageBox.Show("Niste odabrali prostoriju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (equipmentTab.IsSelected)//tab oprema
            {
                if (staticEquipmentTab.IsSelected)
                {
                    if (equipmentTable.equipmentTable.SelectedItem != null)
                        EditEquipment.getInstance((Equipment)this.equipmentTable.equipmentTable.SelectedItem).Show();
                    else
                        MessageBox.Show("Niste odabrali opremu!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (dynamicEquipmentTab.IsSelected)
                {
                    if(dynamicEquipmentTable.dynamicEquipmentTable.SelectedItem != null)
                        EditEquipment.getInstance((Equipment)this.dynamicEquipmentTable.dynamicEquipmentTable.SelectedItem).Show();
                    else
                        MessageBox.Show("Niste odabrali opremu!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void renovationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(roomsUserControl.allRoomsTable.SelectedItem != null)
                RoomRenovationWindow.GetInstance((Room)roomsUserControl.allRoomsTable.SelectedItem).ShowDialog();
            else
                MessageBox.Show("Izaberite prostoriju iz tabele!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void newMedicineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewMedicineWindow.GetInstance().Show();
        }

        private void removeMedicineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (medicineTableUserControl.medicineTable.SelectedItem != null)
            {
                MedicineController.GetInstance().DeleteMedicine((Medicine)medicineTableUserControl.medicineTable.SelectedItem);
                medicineTableUserControl.RefreshTable();
                MessageBox.Show("Izabrani lek je sada obrisan iz sistema.", "Brisanje leka", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Izaberite lek iz tabele!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
