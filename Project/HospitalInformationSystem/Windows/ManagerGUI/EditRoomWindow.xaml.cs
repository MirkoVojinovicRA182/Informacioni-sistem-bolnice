using ControlzEx.Theming;
using HospitalInformationSystem.Controller;
using HospitalInformationSystem.DTO;
using HospitalInformationSystem.Utility;
using MahApps.Metro.Controls;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class EditRoomWindow : MetroWindow
    {
        Room selectedRoom;
        private static EditRoomWindow instance = null;
        private Hashtable roomEquipment;
        private Hashtable reducedEquipment;
        private Hashtable newEquipment;
        string selectedEquipmentName;
        string selectedEquipmentQuantity;
        public EditRoomWindow() { }
        public static EditRoomWindow getInstance(Room selectedRoom)
        {
            if (instance == null)
                instance = new EditRoomWindow(selectedRoom);
            return instance;
        }
        private EditRoomWindow(Room selectedRoom)
        {
            InitializeComponent();
            InitializeFields(selectedRoom);
            LoadTypeComboBox();
            FillComponents();
            LoadRoomEquipment();
            RefreshDynamicEquipmentListBox();
            RefreshStaticEquipmentListBox();
            ManipulateComponents();
        }
        private void InitializeFields(Room selectedRoom)
        {
            this.selectedRoom = selectedRoom;
            roomEquipment = new Hashtable();
            reducedEquipment = new Hashtable();
            newEquipment = new Hashtable();
        }
        private void LoadTypeComboBox()
        {
            string[] typesOfRoom = { Constants.OPERATION_ROOM , Constants.REST_ROOM, Constants.ROOM_WITH_BEDS, Constants.HOSPITALIZATION_ROOM,
            Constants.OFFICE, Constants.EXAMINATION_ROOM, Constants.MAGACINE};
            typeComboBox.ItemsSource = new List<String>(typesOfRoom);
        }
        public void FillComponents()
        {
            idTextBox.Text = selectedRoom.Id.ToString();
            nameTextBox.Text = selectedRoom.Name;
            floorTextBox.Text = selectedRoom.Floor.ToString();
            fiilTypeComboBox(selectedRoom.Type);
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
        public void LoadRoomEquipment()
        {
            roomEquipment.Clear();
            foreach (DictionaryEntry de in selectedRoom.EquipmentInRoom.Equipment)
                roomEquipment.Add(de.Key, de.Value);
        }
        public void RefreshDynamicEquipmentListBox()
        {
            dynamicEquipmentListBox.ItemsSource = null;
            dynamicEquipmentListBox.ItemsSource = getRoomEquipment(TypeOfEquipment.Dynamic);
        }
        public void RefreshStaticEquipmentListBox()
        {
            staticEquipmentListBox.ItemsSource = null;
            staticEquipmentListBox.ItemsSource = getRoomEquipment(TypeOfEquipment.Static);
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
        private void ManipulateComponents()
        {
            if (selectedRoom.Name.Equals("Magacin"))
            {
                addDynamicButton.IsEnabled = false;
                removeDynamicButton.IsEnabled = false;
                addStaticButton.IsEnabled = false;
                additionOfDynamicEquipmentButton.IsEnabled = false;
                typeComboBox.IsEnabled = false;
            }
        }
        private void changeRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if(TryUpdateRoom())
            {
                ManagerMainWindow.getInstance().roomsUserControl.refreshTable();
                MessageBox.Show("Informacije o prostoriji su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                changeRoomButton.IsEnabled = false;
                this.Close();
            }
        }
        private bool TryUpdateRoom()
        {
            if (CheckInputControls())
            {
                selectedRoom.UpdateProperties(new RoomDTO(int.Parse(idTextBox.Text), nameTextBox.Text, int.Parse(floorTextBox.Text), LoadTypeOfRoomFromComboBox((string)typeComboBox.SelectedValue)));
                return true;
            }
            return false;
        }
        private bool CheckInputControls()
        {
            int id = int.TryParse(idTextBox.Text, out id) ? id : 0;
            int floor = int.TryParse(floorTextBox.Text, out floor) ? floor : 0;
            if (id == 0)
            {
                MessageBox.Show("Pogrešan unos šifre!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (RoomController.GetInstance().RoomExists(id) && id != selectedRoom.Id)
            {
                MessageBox.Show("U sistemu postoji prostorija sa ovom šifrom!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.Compare(nameTextBox.Text, "") == 0)
            {
                MessageBox.Show("Polje za unos naziva ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (floor == 0)
            {
                MessageBox.Show("Pogrešan unos sprata!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private TypeOfRoom LoadTypeOfRoomFromComboBox(string selectedValue)
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
        private void addStaticButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance(roomEquipment, Constants.STATIC_EQUIPMENT, Constants.EDIT_ROOM_WINDOW).Show();
        }
        private void moveStaticEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (staticEquipmentListBox.SelectedItem != null)
            {
                TryOpenEquipmentDeploymentWindow();
            }
            else
                MessageBox.Show("Niste odabrali opremu!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void TryOpenEquipmentDeploymentWindow()
        {
            if (RoomController.GetInstance().EquipmentExistInRoom(EquipmentController.getInstance().getEquipmentIdByName(SplitListBoxSelectedValue(staticEquipmentListBox)[0]), selectedRoom.EquipmentInRoom.Equipment))
                StaticEquipmentDeploymentWindow.getInstance(selectedRoom, EquipmentController.getInstance().getEquipmentIdByName(SplitListBoxSelectedValue(staticEquipmentListBox)[0])).Show();
            else
                MessageBox.Show("Prvo dodajte opremu, zatim zakažite njeno premeštanje!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private string[] SplitListBoxSelectedValue(ListBox relevantListBox)
        {
            string nameOfSelectedEquipment = (string)relevantListBox.SelectedItem;
            string[] separator = { " x", };
            string[] atributesOfSelectedEquipment = nameOfSelectedEquipment.Split(separator, StringSplitOptions.None);
            string[] keyValueStructure = { atributesOfSelectedEquipment[0], atributesOfSelectedEquipment[1] };
            return keyValueStructure;
        }
        private void addDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance(roomEquipment, Constants.DYNAMIC_EQUIPMENT, Constants.EDIT_ROOM_WINDOW).Show();
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
            RefreshDynamicEquipmentListBox();
            RefreshStaticEquipmentListBox();
        }
        private void additionOfDynamicEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (dynamicEquipmentListBox.SelectedItem != null)
            {
                SupplementingDynamicEquipmentWindow.GetInstance(selectedRoom, EquipmentController.getInstance().getEquipmentIdByName(SplitListBoxSelectedValue(dynamicEquipmentListBox)[0])).ShowDialog();
            }
        }
        private void removeDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            if (dynamicEquipmentListBox.SelectedItem != null)
            {
                LoadSelectedEquipmentAttributes();
                TryReduceDynamicEquipment();
                RefreshDynamicEquipmentListBox();
            }
        }
        private void LoadSelectedEquipmentAttributes()
        {
            selectedEquipmentName = SplitListBoxSelectedValue(dynamicEquipmentListBox)[0];
            selectedEquipmentQuantity = SplitListBoxSelectedValue(dynamicEquipmentListBox)[1];
        }
        private void TryReduceDynamicEquipment()
        {
            InsertQuantityOfEquipmentForRemovingWindow.getInstance(int.Parse(selectedEquipmentQuantity)).ShowDialog();
            if (InsertQuantityOfEquipmentForRemovingWindow.itSubmitted)
            {
                ReduceQuantity();
            }
        }
        private void ReduceQuantity()
        {
            int currentQuantity = int.Parse(selectedEquipmentQuantity);
            int removedQuantity = InsertQuantityOfEquipmentForRemovingWindow.getQuantity();
            if ((currentQuantity - removedQuantity) == 0)
            {
                this.roomEquipment.Remove(selectedEquipmentName);
                this.newEquipment.Remove(selectedEquipmentName);
            }
            else roomEquipment[EquipmentController.getInstance().getEquipmentIdByName(selectedEquipmentName)] = currentQuantity - removedQuantity;
            if (reducedEquipment.Contains(selectedEquipmentName)) reducedEquipment.Remove(selectedEquipmentName);
            if (selectedRoom.EquipmentInRoom.Equipment.Contains(selectedEquipmentName)) reducedEquipment.Add(selectedEquipmentName, removedQuantity);
        }
        private void equipmentApplyButton_Click(object sender, RoutedEventArgs e)
        {
            selectedRoom.EquipmentInRoom.Equipment = roomEquipment;
            ReduceMagacineEquipmentQuantity();
            if(reducedEquipment.Count != 0)
                ReduceRoomEquipmentQuantity();
            MessageBox.Show("Informacije o opremi prostorije su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
        private void ReduceMagacineEquipmentQuantity()
        {
            foreach (DictionaryEntry de in newEquipment)
            {
                RoomController.GetInstance().GetMagacine().EquipmentInRoom.ReduceEquipmentQuantity(de.Key.ToString(), (int)de.Value);
            }
        }
        private void ReduceRoomEquipmentQuantity()
        {
            foreach (DictionaryEntry de in reducedEquipment)
            {
                EquipmentController.getInstance().findEquipmentById(de.Key.ToString()).ReduceQuantity((int)de.Value);
            }
        }

        private void window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
