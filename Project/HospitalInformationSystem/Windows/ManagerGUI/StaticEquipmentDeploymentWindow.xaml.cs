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
        private int quantityForMoving;
        private ObservableCollection<Room> roomList;
        private int quantityOfSelectedEquipment;
        private string idOfSelectedEquipment;
        public static StaticEquipmentDeploymentWindow getInstance(Room currentRoom, int quantityOfSelectedEquipment, string idOfSelectedEquipment)
        {
            if (instance == null)
                instance = new StaticEquipmentDeploymentWindow(currentRoom, quantityOfSelectedEquipment, idOfSelectedEquipment);
            return instance;
        }
        private StaticEquipmentDeploymentWindow(Room currentRoom, int quantityOfSelectedEquipment, string idOfSelectedEquipment)
        {
            InitializeComponent();
            this.currentRoom = currentRoom;
            this.quantityOfSelectedEquipment = quantityOfSelectedEquipment;
            this.idOfSelectedEquipment = idOfSelectedEquipment;
            currentRoomTextBlock.Text = currentRoom.Name;
            LoadRoomComboBox();
            LoadTimeComboBox();
        }


        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            nextRoom = (Room)nextRoomComboBox.SelectedItem;
            quantityForMoving = int.TryParse(quantityTextBox.Text, out quantityForMoving) ? quantityForMoving : 0;
            if (CheckQuantityForMoving())
            {
                DateTime dateForMovingEquipment = CreateDateTimeObject();
                CreateThreadForMovingEquipment(dateForMovingEquipment);
                SuccessMovingWindow.getInstance(quantityOfSelectedEquipment, quantityOfSelectedEquipment - quantityForMoving).Show();
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

        private bool CheckQuantityForMoving()
        {
            return (quantityForMoving > 0 && quantityForMoving <= quantityOfSelectedEquipment);
        }

        private DateTime CreateDateTimeObject()
        {
            string selectedTime = (string)timeComboBox.SelectedItem;
            string[] separator = { ":", };
            string[] atributesOfSelectedTime = selectedTime.Split(separator, StringSplitOptions.None);
            DateTime selectedDate = (DateTime)datePicker.SelectedDate;
            return new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 
                int.Parse(atributesOfSelectedTime[0]), int.Parse(atributesOfSelectedTime[1]), 00);
        }

        private void CreateThreadForMovingEquipment(DateTime dateForMovingEquipment)
        {
            Thread tr = new Thread(() =>
            {
                //bool isMoved = false;
                while (true)
                {
                    DateTime now = DateTime.Now;
                    if (DateTime.Equals(dateForMovingEquipment.Year, now.Year) && DateTime.Equals(dateForMovingEquipment.Month, now.Month) && DateTime.Equals(dateForMovingEquipment.Day, now.Day) && DateTime.Equals(dateForMovingEquipment.Hour, now.Hour) && DateTime.Equals(dateForMovingEquipment.Minute, now.Minute))
                    {

                        //brisanje opreme iz trenutne prostorije
                        RoomController.GetInstance().ChangeStaticEquipmentState(currentRoom, quantityOfSelectedEquipment, quantityForMoving, idOfSelectedEquipment);
                        //dodavanje opreme u zeljenu prostoriju
                        RoomController.GetInstance().MoveStaticEqToNextRoom(nextRoom, quantityForMoving, idOfSelectedEquipment);
                        break;
                    }
                }
            });

            tr.Start();
        }

        private void LoadRoomComboBox()
        {
            roomList = new ObservableCollection<Room>(RoomController.GetInstance().GetRooms());
            roomList.Remove(currentRoom);
            nextRoomComboBox.ItemsSource = null;
            nextRoomComboBox.ItemsSource = roomList;
        }

        private void LoadTimeComboBox()
        {
            timeComboBox.ItemsSource = null;
            timeComboBox.ItemsSource = MainWindow.GetTimeList();
        }
    }
}
