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

namespace HospitalInformationSystem.Windows.Manager
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
            InitializeComponent();
            roomRepository = new RoomRepository();

            EquipmentController.getInstance().loadFromFile();
            RoomController.getInstance().loadFromFile();

            roomsTable.refreshTable();
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
            roomsTable.refreshTable();
            equipmentTable.refreshTable();
            dynamicEquipmentTable.refreshTable();
        }

        private void staticDynamicTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roomsTable.refreshTable();
            equipmentTable.refreshTable();
            dynamicEquipmentTable.refreshTable();
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (roomTab.IsSelected) //tab prostorije
            {
                RoomController.getInstance().deleteRoom((Room)this.roomsTable.allRoomsTable.SelectedItem);
                moveEquipmentInMagacine();
                ManagerMainWindow.getInstance().roomsTable.refreshTable();
                MessageBox.Show("Izabrana prostorija je sada obrisana iz sistema.", "Brisanje prostorije", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (equipmentTab.IsSelected)//tab oprema
            {
                Equipment selectedEquipment = null;
                if (staticEquipmentTab.IsSelected)
                {
                    selectedEquipment = (Equipment)this.equipmentTable.equipmentTable.SelectedItem;
                }
                else if (dynamicEquipmentTab.IsSelected)
                {
                    selectedEquipment = (Equipment)this.dynamicEquipmentTable.dynamicEquipmentTable.SelectedItem;
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
            Room selectedRoom = (Room)this.roomsTable.allRoomsTable.SelectedItem;
            foreach (DictionaryEntry de in selectedRoom.Equipment)
                EquipmentController.getInstance().moveEquipmentInMagacine(de.Key.ToString(), (int)de.Value);
        }


        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (roomTab.IsSelected) //tab prostorije
            {
                EditRoomWindow.getInstance((Room)this.roomsTable.allRoomsTable.SelectedItem).Show();
            }
            else if (equipmentTab.IsSelected)//tab oprema
            {
                if(staticEquipmentTab.IsSelected)
                    EditEquipment.getInstance((Equipment)this.equipmentTable.equipmentTable.SelectedItem).Show();
                else if(dynamicEquipmentTab.IsSelected)
                    EditEquipment.getInstance((Equipment)this.dynamicEquipmentTable.dynamicEquipmentTable.SelectedItem).Show();
            }
        }
    }
}
