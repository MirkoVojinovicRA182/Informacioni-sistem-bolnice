using HospitalInformationSystem.Controller;
using HospitalInformationSystem.DTO;
using HospitalInformationSystem.Utility;
using MahApps.Metro.Controls;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static HospitalInformationSystem.Utility.Constants;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for DetailEquipmentViewUserControl.xaml
    /// </summary>
    public partial class DetailEquipmentViewUserControl : MetroContentControl
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
        private void LoadComboBoxes()
        {
            LoadRoomComboBox();
            LoadTypeOfEquipmentListBox();
        }
        private void LoadRoomComboBox()
        {
            roomsList = new ObservableCollection<Room>(RoomController.GetInstance().GetRooms());
            locationComboBox.ItemsSource = null;
            locationComboBox.ItemsSource = roomsList;
        }
        private void LoadTypeOfEquipmentListBox()
        {
            List<String> typeOfEquipmentList = new List<String>();
            typeOfEquipmentList.Add(Constants.STATIC_EQUIPMENT);
            typeOfEquipmentList.Add(Constants.DYNAMIC_EQUIPMENT);
            typeComboBox.ItemsSource = null;
            typeComboBox.ItemsSource = typeOfEquipmentList;
        }
        public void LoadList()
        {
            equipmentList = new ObservableCollection<DetailEquipmentDTO>();
            foreach (Room room in RoomController.GetInstance().GetRooms())
            {
                foreach (DictionaryEntry de in room.EquipmentInRoom.Equipment)
                {
                    Equipment equipment = EquipmentController.getInstance().findEquipmentById(de.Key.ToString());
                    equipmentList.Add(new DetailEquipmentDTO(equipment.Name, equipment.GetStringType, (int)de.Value, room.Name));
                }
            }
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
            if (nameTextBox.Text != "") FindEquipmentByParameter(EquipmentSearchParameters.NAME);
            if (typeComboBox.SelectedItem != null) FindEquipmentByParameter(EquipmentSearchParameters.TYPE);
            if (locationComboBox.SelectedItem != null) FindEquipmentByParameter(EquipmentSearchParameters.LOCATION);
            if (stateTextBox.Text != "" && GetQuantityParseResult() != 0) FindEquipmentByParameter(EquipmentSearchParameters.MIN_STATE);
            else return;
        }
        private void FindEquipmentByParameter(EquipmentSearchParameters filterParameter)
        {
            for (int i = equipmentList.Count - 1; i > -1; i--)
            {
                if (GenerateCondition(filterParameter, equipmentList[i]))
                    equipmentList.Remove(equipmentList[i]);
            }
        }
        private bool GenerateCondition(EquipmentSearchParameters filterParameter, DetailEquipmentDTO equipment)
        {
            if (filterParameter.Equals(EquipmentSearchParameters.NAME)) return !string.Equals(equipment.Name, nameTextBox.Text);
            else if (filterParameter.Equals(EquipmentSearchParameters.TYPE)) return !string.Equals(equipment.Type, (string)typeComboBox.SelectedItem);
            else if (filterParameter.Equals(EquipmentSearchParameters.MIN_STATE)) return equipment.State < int.Parse(stateTextBox.Text);
            else return !string.Equals(equipment.Location, ((Room)locationComboBox.SelectedItem).Name);
        }
        private int GetQuantityParseResult()
        {
            int quantity = int.TryParse(stateTextBox.Text, out quantity) ? quantity : 0;
            return quantity;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            LoadList();
            RefreshTable();
            ClearControlsInput();
        }
        private void ClearControlsInput()
        {
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
