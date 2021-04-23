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
using HospitalInformationSystem.Windows.Manager;
using System.Collections;
using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Service;

namespace HospitalInformationSystem.Windows.Manager
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

        private Hashtable equipment;
        //pamti svu opremu koja je u trenutnoj sesiji dodata prostoriji
        private Hashtable newEquipment;


        private EditRoomWindow(Room selectedRoom)
        {
            InitializeComponent();
            this.selectedRoom = selectedRoom;
            equipment = new Hashtable();
            allDistinctions = new Hashtable();
            newEquipment = new Hashtable();
            loadTypeComboBox();
            loadRoom();
        }

        public static EditRoomWindow getInstance(Room selectedRoom)
        {
            if (instance == null)
                instance = new EditRoomWindow(selectedRoom);
            return instance;
        }

        private void addDynamicButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance(equipment, "dinamicka", "editRoom").Show();
        }

        private void addStaticButton_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentToRoomWindow.getInstance(equipment, "staticka", "editRoom").Show();
        }


        public void addEquipment(string id, int quantity)
        {
            try
            {
                newEquipment.Add(id, quantity);
                equipment.Add(id, quantity);
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

                StaticEquipmentDeploymentWindow.getInstance(selectedRoom, value, key).Show();
            }
            else
                MessageBox.Show("Niste odabrali opremu!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);

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


                InsertQuantityOfEquipmentForRemovingWindow.getInstance().ShowDialog();

                if (InsertQuantityOfEquipmentForRemovingWindow.itSubmitted)
                {
                    //int currentQuantity = (int)de.Value;
                    int currentQuantity = value;
                    int removedQuantity = InsertQuantityOfEquipmentForRemovingWindow.getQuantity();
                    distinction = currentQuantity - removedQuantity;
                    if (distinction == 0)
                    {
                        //this.equipment.Remove(de.Key);
                        this.equipment.Remove(key);
                        this.newEquipment.Remove(key);
                    }
                    else
                        //equipment[de.Key] = distinction; //razlika izmedju stare kolicine i kolicine koja zeli da se ukloni iz sistema
                        equipment[key] = distinction;

                    //allDistinctions.Add(de.Key, removedQuantity);
                    if (allDistinctions.Contains(key))
                    {
                        allDistinctions.Remove(key);
                    }

                    allDistinctions.Add(key, removedQuantity);

                    refreshDynamicEquipmentListBox();
                    
                }

            }
            else
                MessageBox.Show("Niste odabrali opremu!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void changeRoomButton_Click(object sender, RoutedEventArgs e)
        {
            RoomController.getInstance().changeRoom(selectedRoom, int.Parse(idTextBox.Text), nameTextBox.Text, getType(typeComboBox.SelectedIndex), int.Parse(floorTextBox.Text), equipment);
            //promena usled dodavanja neke nove opreme
            changeQuantityInMagacineOfDynamicEquipment();
            //promena usled eventualnog brisanja opreme
            changeQuantityOfDynamicEquipment();
            ManagerMainWindow.getInstance().roomsTable.refreshTable();
            this.Close();
            MessageBox.Show("Informacije o prostoriji su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

            foreach (DictionaryEntry de in selectedRoom.Equipment)
                equipment.Add(de.Key, de.Value);

            //equipment = selectedRoom.Equipment;
            refreshDynamicEquipmentListBox();
            refreshStaticEquipmentListBox();
        }

        private void refreshDynamicEquipmentListBox()
        {
            dynamicEquipmentListBox.ItemsSource = null;
            dynamicEquipmentListBox.ItemsSource = loadDynamicEquimpentInListBox();
        }

        private void refreshStaticEquipmentListBox()
        {
            staticEquipmentListBox.ItemsSource = null;
            staticEquipmentListBox.ItemsSource = loadStaticEquimpentInListBox();
        }

        private void changeQuantityInMagacineOfDynamicEquipment()
        {
            foreach (DictionaryEntry de in newEquipment)
            {
                EquipmentController.getInstance().changeQuantityInMagacine(de.Key.ToString(), (int)de.Value);
            }
        }

        private void changeQuantityOfDynamicEquipment()
        {
            foreach (DictionaryEntry de in allDistinctions)
            {
                EquipmentController.getInstance().removeQuantity(de.Key.ToString(), (int)de.Value);
            }
        }

        private List<String> loadDynamicEquimpentInListBox()
        {
            List<String> list = new List<String>();
            foreach (DictionaryEntry de in equipment)
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
            foreach (DictionaryEntry de in equipment)
            {
                string id = EquipmentController.getInstance().getEquipmentName(de.Key.ToString());
                if (EquipmentController.getInstance().getEquipmentType(de.Key.ToString()) == TypeOfEquipment.Static)
                    list.Add(id + " x" + de.Value.ToString());
            }

            return list;
        }
    }
}
