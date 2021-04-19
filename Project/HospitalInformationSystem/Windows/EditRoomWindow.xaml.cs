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

using Model;
using BusinessLogic;
using HospitalInformationSystem.Windows.Manager;
using System.Collections;
using HospitalInformationSystem.Controller;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window
    {
        Room selectedRoom;
        private static EditRoomWindow instance = null;
        private Hashtable equipment;
        int distinction;
        //pamti svu opremu koja cija je kolicina u nekom momentu bila umanjena
        private Hashtable allDistinctions;
        private EditRoomWindow(Room selectedRoom)
        {
            InitializeComponent();
            this.selectedRoom = selectedRoom;
            loadTypeComboBox();
            loadRoom();
            allDistinctions = new Hashtable();
        }

        public static EditRoomWindow getInstance(Room selectedRoom)
        {
            if (instance == null)
                instance = new EditRoomWindow(selectedRoom);
            return instance;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void changeRoomButton_Click(object sender, RoutedEventArgs e)
        {
            RoomManagement management = new RoomManagement();
            management.changeRoom(selectedRoom, int.Parse(idTextBox.Text), nameTextBox.Text, getType(typeComboBox.SelectedIndex), int.Parse(floorTextBox.Text), equipment);
            changeStateInMagacineOfDynamicEquipment();
            ManagerMainWindow.getInstance().roomsTable.refreshTable();
            MessageBox.Show("Informacije o prostoriji su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void loadRoom()
        {
            idTextBox.Text = selectedRoom.Id.ToString();
            nameTextBox.Text = selectedRoom.Name;
            floorTextBox.Text = selectedRoom.Floor.ToString();
            fiilTypeComboBox(selectedRoom.Type);
            equipment = selectedRoom.Equipment;
            refreshDynamicEquipmentListBox();
        }

        private void removeDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            if (dynamicEquipmentListBox.SelectedItem != null)
            {
                DictionaryEntry de = (DictionaryEntry)dynamicEquipmentListBox.SelectedItem;
 

                InsertQuantityOfEquipmentForRemovingWindow.getInstance().ShowDialog();

                if (InsertQuantityOfEquipmentForRemovingWindow.itSubmitted)
                {
                    int currentQuantity = (int)de.Value;
                    int removedQuantity = InsertQuantityOfEquipmentForRemovingWindow.getQuantity();
                    distinction = currentQuantity - removedQuantity;
                    equipment[de.Key] = distinction; //razlika izmedju stare kolicine i kolicine koja zeli da se ukloni iz sistema

                    allDistinctions.Add(de.Key, removedQuantity);

                    refreshDynamicEquipmentListBox();
                }

            }
            else
                MessageBox.Show("Niste odabrali opremu!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void refreshDynamicEquipmentListBox()
        {
            dynamicEquipmentListBox.ItemsSource = null;
            dynamicEquipmentListBox.ItemsSource = equipment;
        }

        private void addDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance("staticka", "editRoom").Show();
        }

        public void addDynamicEquipment(string id, int quantity)
        {
            try
            {
                equipment.Add(id, quantity);
            }
            catch (Exception e)
            {
                MessageBox.Show("Već ste uneli ovu opremu!Ako ste pogrešili sa prvobitnim unosom, prvo uklonite, pa zatim ponovo unesite opremu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            refreshDynamicEquipmentListBox();
        }

        private void changeStateInMagacineOfDynamicEquipment()
        {
            foreach (DictionaryEntry de in allDistinctions)
            {
                EquipmentController.getInstance().removeQuantity(de.Key.ToString(), (int)de.Value);
            }
        }
    }
}
