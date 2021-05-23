﻿using HospitalInformationSystem.Controller;
using HospitalInformationSystem.DTO;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window
    {
        Room selectedRoom;
        private static EditRoomWindow instance = null;
        int distinction;
        //pamti svu opremu koja cija je kolicina u nekom momentu bila umanjena
        private Hashtable allDistinctions;

        private Hashtable roomEquipment;
        //pamti svu opremu koja je u trenutnoj sesiji dodata prostoriji
        private Hashtable newEquipment;


        private EditRoomWindow(Room selectedRoom)
        {
            InitializeComponent();
            this.selectedRoom = selectedRoom;
            roomEquipment = new Hashtable();
            allDistinctions = new Hashtable();
            newEquipment = new Hashtable();
            //checkEquipment();
            loadTypeComboBox();
            loadRoom();
            equipmentApplyButton.IsEnabled = false;

            if(string.Equals(selectedRoom.Name, "Magacin"))
            {
                addDynamicButton.IsEnabled = false;
                removeDynamicButton.IsEnabled = false;
                addStaticButton.IsEnabled = false;
                additionOfDynamicEquipmentButton.IsEnabled = false;
                typeComboBox.IsEnabled = false;
            }
        }

        public static EditRoomWindow getInstance(Room selectedRoom)
        {
            if (instance == null)
                instance = new EditRoomWindow(selectedRoom);
            return instance;
        }

        private void addDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance(roomEquipment, "dinamicka", "editRoom").Show();
        }

        private void addStaticButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance(roomEquipment, "staticka", "editRoom").Show();
        }


        public void addEquipment(string id, int quantity)
        {
            try
            {
                newEquipment.Add(id, quantity);
                roomEquipment.Add(id, quantity);
            }
            catch (Exception e)
            {
                MessageBox.Show("Već ste uneli ovu opremu!Ako ste pogrešili sa prvobitnim unosom, prvo uklonite, pa zatim ponovo unesite opremu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            refreshDynamicEquipmentListBox();
            refreshStaticEquipmentListBox();
        }


        private void moveStaticEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (staticEquipmentListBox.SelectedItem != null)
            {
             
                string nameOfSelectedEquipment = (string)staticEquipmentListBox.SelectedItem;

                string[] separator = { " x", };

                string[] atributesOfSelectedEquipment = nameOfSelectedEquipment.Split(separator, StringSplitOptions.None);

                string key = EquipmentController.getInstance().getEquipmentId(atributesOfSelectedEquipment[0]);

                int value = int.Parse(atributesOfSelectedEquipment[1]);

                if (RoomController.GetInstance().EquipmentExistInRoom(key, selectedRoom.EquipmentInRoom.Equipment))
                    StaticEquipmentDeploymentWindow.getInstance(selectedRoom, value, key).Show();
                else
                    MessageBox.Show("Prvo dodajte opremu, zatim zakažite njeno premeštanje!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                MessageBox.Show("Niste odabrali opremu!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
        private void removeDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            if (dynamicEquipmentListBox.SelectedItem != null)
            {
                //DictionaryEntry de = (DictionaryEntry)dynamicEquipmentListBox.SelectedItem;

                string nameOfSelectedEquipment = (string)dynamicEquipmentListBox.SelectedItem;

                string[] separator = { " x",};

                string[] atributesOfSelectedEquipment = nameOfSelectedEquipment.Split(separator, StringSplitOptions.None);

                string key = EquipmentController.getInstance().getEquipmentId(atributesOfSelectedEquipment[0]);
                int value = int.Parse(atributesOfSelectedEquipment[1]);


                InsertQuantityOfEquipmentForRemovingWindow.getInstance(value).ShowDialog();

                if (InsertQuantityOfEquipmentForRemovingWindow.itSubmitted)
                {
                    int currentQuantity = value;
                    int removedQuantity = InsertQuantityOfEquipmentForRemovingWindow.getQuantity();
                    distinction = currentQuantity - removedQuantity;
                    if (distinction == 0)
                    {
                        this.roomEquipment.Remove(key);
                        this.newEquipment.Remove(key);
                    }
                    else
                        roomEquipment[key] = distinction;

                    if (allDistinctions.Contains(key))
                    {
                        allDistinctions.Remove(key);
                    }

                    if(selectedRoom.EquipmentInRoom.Equipment.Contains(key))
                        allDistinctions.Add(key, removedQuantity);

                    refreshDynamicEquipmentListBox();
                    
                }

            }
            else
                MessageBox.Show("Niste odabrali opremu!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void changeRoomButton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.TryParse(idTextBox.Text, out id) ? id : 0;
            int floor = int.TryParse(floorTextBox.Text, out floor) ? floor : 0;

            if (id == 0)
                MessageBox.Show("Pogrešan unos šifre!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (RoomController.GetInstance().RoomExists(id) && id != selectedRoom.Id)
                MessageBox.Show("U sistemu postoji prostorija sa ovom šifrom!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (string.Compare(nameTextBox.Text, "") == 0)
                MessageBox.Show("Polje za unos naziva ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (floor == 0)
                MessageBox.Show("Pogrešan unos sprata!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                selectedRoom.UpdateProperties(new RoomDTO(int.Parse(idTextBox.Text), nameTextBox.Text, int.Parse(floorTextBox.Text), getType(typeComboBox.SelectedIndex)));
                ManagerMainWindow.getInstance().roomsUserControl.refreshTable();
                MessageBox.Show("Informacije o prostoriji su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                changeRoomButton.IsEnabled = false;
                this.Close();
            }
        }

        private void equipmentApplyButton_Click(object sender, RoutedEventArgs e)
        {
            //RoomController.GetInstance().SetRoomEquipment(selectedRoom, equipment);
            selectedRoom.EquipmentInRoom.Equipment = roomEquipment;
            //promena usled dodavanja neke nove opreme
            changeQuantityInMagacineOfEquipment();
            //promena usled eventualnog brisanja opreme
            changeQuantityOfEquipment();
            //ManagerMainWindow.getInstance().roomsUserControl.refreshTable();
            MessageBox.Show("Informacije o opremi prostorije su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            equipmentApplyButton.IsEnabled = false;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private TypeOfRoom getType(int selectedValue)
        {
            TypeOfRoom type = 0;
            if (selectedValue == 0)
                type = TypeOfRoom.OperationRoom;
            else if (selectedValue == 1)
                type = TypeOfRoom.RestRoom;
            else if (selectedValue == 2)
                type = TypeOfRoom.RoomWithBeds;
            else if (selectedValue == 3)
                type = TypeOfRoom.HospitalizationRoom;
            else if (selectedValue == 4)
                type = TypeOfRoom.Office;
            else if (selectedValue == 5)
                type = TypeOfRoom.ExaminationRoom;

            return type;

        }

        private void loadTypeComboBox()
        {
            var list = new List<String>();

            list.Add("Operaciona sala");
            list.Add("Prostorija za odmor");
            list.Add("Soba sa krevetima");
            list.Add("Sala za hospitalizaciju");
            list.Add("Kancelarija");
            list.Add("Prostorija za preglede");

            typeComboBox.ItemsSource = list;
        }

        private void fiilTypeComboBox(TypeOfRoom type)
        {
            if (type == TypeOfRoom.ExaminationRoom)
                typeComboBox.SelectedIndex = 5;
            else if (type == TypeOfRoom.HospitalizationRoom)
                typeComboBox.SelectedIndex = 3;
            else if (type == TypeOfRoom.Office)
                typeComboBox.SelectedIndex = 4;
            else if (type == TypeOfRoom.OperationRoom)
                typeComboBox.SelectedIndex = 0;
            else if (type == TypeOfRoom.RestRoom)
                typeComboBox.SelectedIndex = 1;
            else if (type == TypeOfRoom.RoomWithBeds)
                typeComboBox.SelectedIndex = 2;
        }

        public void loadRoom()
        {
            idTextBox.Text = selectedRoom.Id.ToString();
            nameTextBox.Text = selectedRoom.Name;
            floorTextBox.Text = selectedRoom.Floor.ToString();
            fiilTypeComboBox(selectedRoom.Type);
            RefreshEquipmentList();
            refreshDynamicEquipmentListBox();
            refreshStaticEquipmentListBox();
        }
        public void RefreshEquipmentList()
        {
            roomEquipment.Clear();
            foreach (DictionaryEntry de in selectedRoom.EquipmentInRoom.Equipment)
                roomEquipment.Add(de.Key, de.Value);
        }

        public void refreshDynamicEquipmentListBox()
        {
            dynamicEquipmentListBox.ItemsSource = null;
            dynamicEquipmentListBox.ItemsSource = loadDynamicEquimpentInListBox();
        }

        private void refreshStaticEquipmentListBox()
        {
            staticEquipmentListBox.ItemsSource = null;
            staticEquipmentListBox.ItemsSource = loadStaticEquimpentInListBox();
        }

        private void changeQuantityInMagacineOfEquipment()
        {
            foreach (DictionaryEntry de in newEquipment)
            {
                RoomController.GetInstance().GetMagacine().EquipmentInRoom.ReduceEquipmentQuantity(de.Key.ToString(), (int)de.Value);
            }
        }

        private void changeQuantityOfEquipment()
        {
            foreach (DictionaryEntry de in allDistinctions)
            {
                EquipmentController.getInstance().findEquipmentById(de.Key.ToString()).ReduceQuantity((int)de.Value);
            }
        }

        private List<String> loadDynamicEquimpentInListBox()
        {
            List<String> list = new List<String>();
            foreach (DictionaryEntry de in roomEquipment)
            {
                string id = EquipmentController.getInstance().getEquipmentName(de.Key.ToString());
                if (EquipmentController.getInstance().getEquipmentType(de.Key.ToString()) == TypeOfEquipment.Dynamic)
                    list.Add(id + " x" + de.Value.ToString());
            }

            return list;
        }

        private List<String> loadStaticEquimpentInListBox()
        {
            List<String> list = new List<String>();
            foreach (DictionaryEntry de in roomEquipment)
            {
                string id = EquipmentController.getInstance().getEquipmentName(de.Key.ToString());
                if (EquipmentController.getInstance().getEquipmentType(de.Key.ToString()) == TypeOfEquipment.Static)
                    list.Add(id + " x" + de.Value.ToString());
            }

            return list;
        }

        public void checkControls()
        {
            int id = int.TryParse(idTextBox.Text, out id) ? id : 0;
            int floor = int.TryParse(floorTextBox.Text, out floor) ? floor : 0;

            if (selectedRoom.Id != id || selectedRoom.Name != nameTextBox.Text || selectedRoom.StringValueOfEnumType != typeComboBox.SelectedItem || selectedRoom.Floor != floor)
                changeRoomButton.IsEnabled = true;
            else
                changeRoomButton.IsEnabled = false;
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkControls();
        }

        private void idTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkControls();
        }

        private void nameTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkControls();
        }

        private void floorTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkControls();
        }
        private void checkEquipment()
        {
            if (newEquipment.Count == 0 && allDistinctions.Count == 0)
                equipmentApplyButton.IsEnabled = false;
            else
                equipmentApplyButton.IsEnabled = true;
        }

        private void dynamicEquipmentListBox_LayoutUpdated(object sender, EventArgs e)
        {
            checkEquipment();
        }

        private void staticEquipmentListBox_LayoutUpdated(object sender, EventArgs e)
        {
            checkEquipment();
        }

        private void additionOfDynamicEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (dynamicEquipmentListBox.SelectedItem != null)
            {
                string nameOfSelectedEquipment = (string)dynamicEquipmentListBox.SelectedItem;
                string[] separator = { " x", };
                string[] atributesOfSelectedEquipment = nameOfSelectedEquipment.Split(separator, StringSplitOptions.None);
                string idOfSelectedEquipment = EquipmentController.getInstance().getEquipmentId(atributesOfSelectedEquipment[0]);
                SupplementingDynamicEquipmentWindow.GetInstance(selectedRoom, idOfSelectedEquipment).ShowDialog();
            }
            else
                MessageBox.Show("Niste odabrali opremu!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
