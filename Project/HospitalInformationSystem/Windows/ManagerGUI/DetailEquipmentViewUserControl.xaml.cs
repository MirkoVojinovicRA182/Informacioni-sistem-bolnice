using HospitalInformationSystem.Controller;
using HospitalInformationSystem.DTO;
using Model;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for DetailEquipmentViewUserControl.xaml
    /// </summary>
    public partial class DetailEquipmentViewUserControl : UserControl
    {
        private ObservableCollection<DetailEquipmentDTO> equipmentList;

        private ObservableCollection<Room> roomsList;

        public DetailEquipmentViewUserControl()
        {
            InitializeComponent();
            LoadAllUserControlComponents();
        }

        public void LoadAllUserControlComponents()
        {
            stateTextBox.Clear();
            nameTextBox.Clear();
            LoadComboBoxes();
            LoadList();
            RefreshTable();
        }
        public void LoadList()
        {
            equipmentList = new ObservableCollection<DetailEquipmentDTO>();

            //prolazak kroz sve prostorije
            foreach (Room room in RoomController.GetInstance().GetRooms())
            {
                foreach (DictionaryEntry de in room.Equipment)
                {
                    Equipment eq = EquipmentController.getInstance().findEquipmentById(de.Key.ToString());
                    equipmentList.Add(new DetailEquipmentDTO(eq.Name, eq.GetStringType, (int)de.Value, room.Name));
                }
            }

            //prolazak kroz magacin
            /*foreach (Equipment eq in EquipmentController.getInstance().getEquipment())
            {
                if (eq.QuantityInMagacine > 0)
                    equipmentList.Add(new DetailEquipmentDTO(eq.Name, eq.GetStringType, eq.QuantityInMagacine, "Magacin"));
            }*/
        }
        private void LoadComboBoxes()
        {
            roomsList = new ObservableCollection<Room>(RoomController.GetInstance().GetRooms());
            locationComboBox.ItemsSource = null;
            locationComboBox.ItemsSource = roomsList;

            List<String> typeOfEquipmentList = new List<String>();
            typeOfEquipmentList.Add("Statička");
            typeOfEquipmentList.Add("Dinamička");
            typeComboBox.ItemsSource = null;
            typeComboBox.ItemsSource = typeOfEquipmentList;
        }
        public void RefreshTable()
        {
            detailEquipmentTable.ItemsSource = null;
            detailEquipmentTable.ItemsSource = equipmentList;
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            LoadList();
            RefreshTable();
            if(nameTextBox.Text != "")
                FindEquipmentByName();
            if(typeComboBox.SelectedItem != null)
                FindEquipmentByType();
            if (stateTextBox.Text != "")
            {
                if (CheckQuantityEntry() == 0)
                {
                    MessageBox.Show("Pogrešan unos stanja!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                FindEquipmentByState();
            }
            if (locationComboBox.SelectedItem != null)
                FindEquipmentByLocation();
            detailEquipmentTable.ItemsSource = null;
            detailEquipmentTable.ItemsSource = equipmentList;
        }
        private int CheckQuantityEntry()
        {
            int quantity = int.TryParse(stateTextBox.Text, out quantity) ? quantity : 0;
            return quantity;
        }
        private void FindEquipmentByName()
        {
            for(int i = equipmentList.Count - 1; i > -1; i--)
            {
                if (!string.Equals(equipmentList[i].Name, nameTextBox.Text))
                    equipmentList.Remove(equipmentList[i]);
            }
        }

        private void FindEquipmentByType()
        {
            for (int i = equipmentList.Count - 1; i > -1; i--)
            {
                if (!string.Equals(equipmentList[i].Type, (string)typeComboBox.SelectedItem))
                    equipmentList.Remove(equipmentList[i]);
            }
        }

        private void FindEquipmentByState()
        {
            for (int i = equipmentList.Count - 1; i > -1; i--)
            {
                if (equipmentList[i].State < int.Parse(stateTextBox.Text))
                    equipmentList.Remove(equipmentList[i]);
            }
        }

        private void FindEquipmentByLocation()
        {
            Room selectedRoom = (Room)locationComboBox.SelectedItem;
            for (int i = equipmentList.Count - 1; i > -1; i--)
            {
                if (!string.Equals(equipmentList[i].Location, selectedRoom.Name))
                    equipmentList.Remove(equipmentList[i]);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            LoadList();
            RefreshTable();
            nameTextBox.Clear();
            typeComboBox.SelectedItem = null;
            stateTextBox.Clear();
            locationComboBox.SelectedItem = null;
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void locationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
