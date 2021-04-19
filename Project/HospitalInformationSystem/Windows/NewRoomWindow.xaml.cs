using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Windows;
using Model;
using HospitalInformationSystem.Windows.Manager;
using System.Collections.ObjectModel;
using HospitalInformationSystem.Controller;
using System.Collections;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for NewRoomWindow.xaml
    /// </summary>
    public partial class NewRoomWindow : Window
    {
        private static NewRoomWindow instance = null;
        private Hashtable equipment;
        private List<Equipment> dynamicEquipment;
        private NewRoomWindow()
        {
            InitializeComponent();
            loadComboBox();
            equipment = new Hashtable();
            dynamicEquipment = new List<Equipment>();
        }

        public static NewRoomWindow getInstance()
        {
            if (instance == null)
                instance = new NewRoomWindow();
            return instance;
        }


        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            createRoom();

            idTextBox.Clear();
            nameTextBox.Clear();
            floorTextBox.Clear();
            typeOfRoomComboBox.SelectedIndex = 0;


            changeQuantityInMagacineOfDynamicEquipment();

            ManagerMainWindow.getInstance().roomsTable.refreshTable();

            MessageBox.Show("Uneta je nova prostorija u sistem.", "Nova prostorija", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void createRoom()
        {
            int id = int.Parse(idTextBox.Text);
            string name = nameTextBox.Text;
            int floor = int.Parse(floorTextBox.Text);
            TypeOfRoom type = loadType((string)typeOfRoomComboBox.SelectedItem);

            RoomManagement roomManagement = new RoomManagement();

            roomManagement.createRoom(floor, id, name, type, equipment);

        }

        private void loadComboBox()
        {
            var list = new List<String>();

            list.Add("Operaciona sala");
            list.Add("Prostorija za odmor");
            list.Add("Soba sa krevetima");
            list.Add("Sala za hospitalizaciju");
            list.Add("Kancelarija");
            list.Add("Prostorija za preglede");

            typeOfRoomComboBox.ItemsSource = list;
            typeOfRoomComboBox.SelectedIndex = 0;
        }

        private TypeOfRoom loadType(string selectedValue)
        {
            TypeOfRoom type = 0;
            if (String.Compare(selectedValue, "Operaciona sala") == 0)
                type = TypeOfRoom.OperationRoom;
            else if (String.Compare(selectedValue, "Prostorija za odmor") == 0)
                type = TypeOfRoom.RestRoom;
            else if (String.Compare(selectedValue, "Soba sa krevetima") == 0)
                type = TypeOfRoom.RoomWithBeds;
            else if (String.Compare(selectedValue, "Sala za hospitalizaciju") == 0)
                type = TypeOfRoom.HospitalizationRoom;
            else if (String.Compare(selectedValue, "Kancelarija") == 0)
                type = TypeOfRoom.Office;
            else if (String.Compare(selectedValue, "Prostorija za preglede") == 0)
                type = TypeOfRoom.ExaminationRoom;

            return type;

        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            instance = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        public void addDynamicEquipment(string id, int quantity)
        {
            try
            {
                this.equipment.Add(id, quantity);
            }
            catch(Exception e)
            {
                MessageBox.Show("Već ste uneli ovu opremu!Ako ste pogrešili sa prvobitnim unosom, prvo uklonite, pa zatim ponovo unesite opremu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            refreshDynamicEquipmentListBox();
        }

        private void addDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance("staticka", "newRoom").Show();
        }

        private void removeDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            if (dynamicEquipmentListBox.SelectedItem != null)
            {
            
                string nameOfSelectedEquipment = (string)dynamicEquipmentListBox.SelectedItem;

                string[] separator = { " " };

                string[] atributesOfSelectedEquipment = nameOfSelectedEquipment.Split(separator, StringSplitOptions.None);

                equipment.Remove(EquipmentController.getInstance().getEquipmentId(atributesOfSelectedEquipment[0]));


                refreshDynamicEquipmentListBox();
            }
            else
                MessageBox.Show("Niste odabrali opremu!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void refreshDynamicEquipmentListBox()
        {
            dynamicEquipmentListBox.ItemsSource = null;
            dynamicEquipmentListBox.ItemsSource = loadEquimpentInListBox();
        }

        private void changeQuantityInMagacineOfDynamicEquipment()
        {
            foreach(DictionaryEntry de in equipment)
            {
                EquipmentController.getInstance().changeQuantityInMagacine(de.Key.ToString(), (int)de.Value);
            }
        }

        private List<String> loadEquimpentInListBox()
        {
            List<String> list = new List<String>();
            foreach(DictionaryEntry de in equipment)
            {
                string id = EquipmentController.getInstance().getEquipmentName(de.Key.ToString());
                list.Add(id + " x" + de.Value.ToString());
            }

            return list;
        }       
    }

}
