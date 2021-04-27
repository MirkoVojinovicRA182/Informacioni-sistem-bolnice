using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for StaticEquipmentDeploymentWindow.xaml
    /// </summary>
    public partial class StaticEquipmentDeploymentWindow : Window
    {
        private static StaticEquipmentDeploymentWindow instance = null;
        private Room currentRoom;
        private Room nextRoom;
        private int quantity;
        private ObservableCollection<Room> roomList;
        private int quantityOfSelectedEquipment;
        private string idOfSelectedEquipment;
        public static StaticEquipmentDeploymentWindow getInstance(Room room, int value, string key)
        {
            if (instance == null)
                instance = new StaticEquipmentDeploymentWindow(room, value, key);
            return instance;
        }
        private StaticEquipmentDeploymentWindow(Room room, int value, string key)
        {
            InitializeComponent();
            currentRoom = room;
            quantityOfSelectedEquipment = value;
            idOfSelectedEquipment = key;
            fillControls();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            nextRoom = (Room)nextRoomComboBox.SelectedItem;
            quantity = int.TryParse(quantityTextBox.Text, out quantity) ? quantity : 0;

            if (quantity != 0 && quantity > 0 && quantity <= quantityOfSelectedEquipment)
            {
                string selectedTime = (string)timeComboBox.SelectedItem;

                string[] separator = { ":", };

                string[] atributesOfSelectedTime = selectedTime.Split(separator, StringSplitOptions.None);

                int hour = int.Parse(atributesOfSelectedTime[0]);

                int minut = int.Parse(atributesOfSelectedTime[1]);

                DateTime selectedDate = (DateTime)datePicker.SelectedDate;

                DateTime myDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, hour, minut, 00);

                Thread tr = new Thread(() =>
                {

                    //bool isMoved = false;
                    while (true)
                    {
                        DateTime now = DateTime.Now;

                        if (DateTime.Equals(myDate.Year, now.Year) && DateTime.Equals(myDate.Month, now.Month) && DateTime.Equals(myDate.Day, now.Day) && DateTime.Equals(myDate.Hour, now.Hour) && DateTime.Equals(myDate.Minute, now.Minute))
                        {
                            MessageBox.Show("Sad bi trebao da pomeri opremu");

                            //brisanje opreme iz trenutne prostorije
                            RoomController.getInstance().changeStaticEquipmentState(currentRoom, quantityOfSelectedEquipment, quantity, idOfSelectedEquipment);

                            //dodavanje opreme u zeljenu prostoriju
                            RoomController.getInstance().moveStaticEqToNextRoom(nextRoom, quantity, idOfSelectedEquipment);

                            break;
                        }
                    }
                });

                tr.Start();

                SuccessMovingWindow.getInstance(quantityOfSelectedEquipment, quantityOfSelectedEquipment - quantity).Show();

                this.Close();

            }
            else
                MessageBox.Show("Pogrešan unos količine!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void fillControls()
        {
            currentRoomTextBlock.Text = currentRoom.Name;
            loadRoomComboBox();
            loadTimeComboBox();
        }

        private void loadRoomComboBox()
        {
            roomList = new ObservableCollection<Room>(RoomController.getInstance().getRooms());

            //brisanje trenutne prostorije iz liste svih potencijalnih prostorija
            roomList.Remove(currentRoom);

            nextRoomComboBox.ItemsSource = null;
            nextRoomComboBox.ItemsSource = roomList;
        }

        private void loadTimeComboBox()
        {
            List<String> times = new List<String>();
            for(int i = 6; i <= 21; i++)
            {
                for(int j = 0; j <= 59; j++)
                {
                    times.Add(i.ToString() + ":" + j.ToString());
                }
            }

            timeComboBox.ItemsSource = null;
            timeComboBox.ItemsSource = times;
        }
    }
}
