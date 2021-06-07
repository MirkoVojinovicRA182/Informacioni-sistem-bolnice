using System;
using System.Collections.Generic;
using System.Windows;
using Model;
using HospitalInformationSystem.Controller;
using System.Collections;
using System.Windows.Controls;
using HospitalInformationSystem.Utility;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class NewRoomWindow : Window
    {
        private static NewRoomWindow instance = null;
        private Hashtable roomEquipment;
        int idOfNewRoom;
        string nameOfNewRoom;
        int floorOfNewRoom;
        TypeOfRoom typeOfNewRoom;
        private NewRoomWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            loadComboBox();
            roomEquipment = new Hashtable();
        }
        public static NewRoomWindow getInstance()
        {
            if (instance == null)
                instance = new NewRoomWindow();
            return instance;
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            MakeRoomAttributes();
            while (TryCreateRoom())
            {
                ManagerMainWindow.getInstance().roomsUserControl.refreshTable();
                this.Close();
                MessageBox.Show("Uneta je nova prostorija u sistem.", "Nova prostorija", MessageBoxButton.OK, MessageBoxImage.Information);
                break;
            }
        }
        private void MakeRoomAttributes()
        {
            idOfNewRoom = int.TryParse(idTextBox.Text, out idOfNewRoom) ? idOfNewRoom : 0;
            nameOfNewRoom = nameTextBox.Text;
            floorOfNewRoom = int.TryParse(floorTextBox.Text, out floorOfNewRoom) ? floorOfNewRoom : 0;
            typeOfNewRoom = loadTypeOfRoomFromComboBox((string)typeOfRoomComboBox.SelectedItem);
        }
        private bool TryCreateRoom()
        {
            if (CheckInputControls())
            {
                RoomController.GetInstance().AddRoomToRoomList(new Room(idOfNewRoom, nameOfNewRoom, floorOfNewRoom, typeOfNewRoom, roomEquipment));
                if(roomEquipment.Count != 0)
                    reduceMagacineEquipmentQuantity();
                return true;
            }
            return false;
        }
        private bool CheckInputControls()
        {
            if (idOfNewRoom == 0)
            {
                MessageBox.Show("Pogrešan unos šifre!", Constants.ERROR_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (RoomController.GetInstance().RoomExists(idOfNewRoom))
            {
                MessageBox.Show("U sistemu postoji prostorija sa ovom šifrom!", Constants.ERROR_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.Compare(nameOfNewRoom, "") == 0)
            {
                MessageBox.Show("Polje za unos naziva ne može biti prazno!", Constants.ERROR_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (typeOfRoomComboBox.SelectedItem == null)
            {
                MessageBox.Show("Niste odabrali tip prostorije!", Constants.ERROR_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (floorOfNewRoom == 0)
            {
                MessageBox.Show("Pogrešan unos sprata!", Constants.ERROR_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void loadComboBox()
        {
            string[] typesOfRoom = { Constants.OPERATION_ROOM , Constants.REST_ROOM, Constants.ROOM_WITH_BEDS, Constants.HOSPITALIZATION_ROOM,
            Constants.OFFICE, Constants.EXAMINATION_ROOM, Constants.MAGACINE};
            typeOfRoomComboBox.ItemsSource = new List<String>(typesOfRoom);
        }
        private TypeOfRoom loadTypeOfRoomFromComboBox(string selectedValue)
        {
            TypeOfRoom type;
            if (selectedValue.CompareTo(Constants.OPERATION_ROOM) == 0)
                type = TypeOfRoom.OperationRoom;
            else if (selectedValue.CompareTo(Constants.REST_ROOM) == 0)
                type = TypeOfRoom.RestRoom;
            else if (selectedValue.CompareTo(Constants.ROOM_WITH_BEDS) == 0)
                type = TypeOfRoom.RoomWithBeds;
            else if (selectedValue.CompareTo(Constants.HOSPITALIZATION_ROOM) == 0)
                type = TypeOfRoom.HospitalizationRoom;
            else if (selectedValue.CompareTo(Constants.OFFICE) == 0)
                type = TypeOfRoom.Office;
            else if (selectedValue.CompareTo(Constants.EXAMINATION_ROOM) == 0)
                type = TypeOfRoom.ExaminationRoom;
            else
                type = TypeOfRoom.Magacine;
            return type;
        }
        public void addEquipmentToRoom(string id, int quantity)
        {
            try
            {
                this.roomEquipment.Add(id, quantity);
            }
            catch(Exception e)
            {
                MessageBox.Show("Već ste uneli ovu opremu! Ako ste pogrešili sa prvobitnim unosom, prvo uklonite, pa zatim ponovo unesite opremu.", Constants.ERROR_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance(roomEquipment, Constants.DYNAMIC_EQUIPMENT, Constants.NEW_ROOM_WINDOW).Show();
        }

        private void addStaticButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance(roomEquipment, Constants.STATIC_EQUIPMENT, Constants.NEW_ROOM_WINDOW).Show();
        }

        private void removeDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            TryRemoveEquipment(dynamicEquipmentListBox);
        }
        private void removeStaticButton_Click(object sender, RoutedEventArgs e)
        {
            TryRemoveEquipment(staticEquipmentListBox);
        }
        private void TryRemoveEquipment(ListBox equipmentListBox)
        {
            if (equipmentListBox.SelectedItem != null)
            {
                string nameOfSelectedEquipment = (string)equipmentListBox.SelectedItem;
                string[] separator = { " " };
                string[] atributesOfSelectedEquipment = nameOfSelectedEquipment.Split(separator, StringSplitOptions.None);
                roomEquipment.Remove(EquipmentController.getInstance().getEquipmentIdByName(atributesOfSelectedEquipment[0]));
                refreshStaticEquipmentListBox();
            }
            else
                MessageBox.Show("Niste odabrali opremu!", Constants.ERROR_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void refreshDynamicEquipmentListBox()
        {
            dynamicEquipmentListBox.ItemsSource = null;
            dynamicEquipmentListBox.ItemsSource = getRoomEquipment(TypeOfEquipment.Dynamic);
        }
        public void refreshStaticEquipmentListBox()
        {
            staticEquipmentListBox.ItemsSource = null;
            staticEquipmentListBox.ItemsSource = getRoomEquipment(TypeOfEquipment.Static);
        }
        private void reduceMagacineEquipmentQuantity()
        {
            foreach(DictionaryEntry de in roomEquipment)
            {
                RoomController.GetInstance().GetMagacine().EquipmentInRoom.ReduceEquipmentQuantity(de.Key.ToString(), (int)de.Value);
            }
        }
        private List<String> getRoomEquipment(TypeOfEquipment typeOfEquipment)
        {
            List<String> equipmentList = new List<String>();
            foreach (DictionaryEntry de in roomEquipment)
            {
                string id = EquipmentController.getInstance().getEquipmentName(de.Key.ToString());
                if (EquipmentController.getInstance().getEquipmentType(de.Key.ToString()).Equals(typeOfEquipment))
                    equipmentList.Add(id + " x" + de.Value.ToString());
            }
            return equipmentList;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }

}
