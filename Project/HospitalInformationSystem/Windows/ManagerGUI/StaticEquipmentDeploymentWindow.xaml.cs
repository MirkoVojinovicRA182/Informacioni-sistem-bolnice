using HospitalInformationSystem.Controller;
using MahApps.Metro.Controls;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class StaticEquipmentDeploymentWindow : MetroWindow
    {
        private static StaticEquipmentDeploymentWindow instance = null;
        private Room currentRoom;
        private Room nextRoom;
        private int quantityForMove;
        private ObservableCollection<Room> potencialRoomForMove;
        private int quantityOfSelectedEquipment;
        private string idOfSelectedEquipment;
        public static StaticEquipmentDeploymentWindow getInstance(Room currentRoom, string idOfSelectedEquipment)
        {
            if (instance == null)
                instance = new StaticEquipmentDeploymentWindow(currentRoom, idOfSelectedEquipment);
            return instance;
        }
        private StaticEquipmentDeploymentWindow(Room currentRoom, string idOfSelectedEquipment)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.currentRoom = currentRoom;
            this.quantityOfSelectedEquipment = (int)currentRoom.EquipmentInRoom.Equipment[idOfSelectedEquipment];
            this.idOfSelectedEquipment = idOfSelectedEquipment;
            currentRoomTextBlock.Text = currentRoom.Name;
            LoadRoomComboBox();
            LoadTimeComboBox();
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            nextRoom = (Room)nextRoomComboBox.SelectedItem;
            quantityForMove = int.TryParse(quantityTextBox.Text, out quantityForMove) ? quantityForMove : 0;
            if (AllInputsAreValid())
            {
                CreateThreadForMovingEquipment(CreateDateTimeObject());
                SuccessMovingWindow.getInstance(quantityOfSelectedEquipment, quantityOfSelectedEquipment - quantityForMove).Show();
                this.Close();
            }
            else
                MessageBox.Show("Niste uneli sve informacije!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
            return (quantityForMove > 0 && quantityForMove <= quantityOfSelectedEquipment);
        }
        private bool CheckTimeInput()
        {
            return timeComboBox.SelectedItem != null;
        }
        private bool CheckDateInput()
        {
            return datePicker.SelectedDate != null;
        }
        private bool AllInputsAreValid()
        {
            return CheckQuantityForMoving() && CheckTimeInput() && CheckDateInput();
        }
        private DateTime CreateDateTimeObject()
        {
            string elementsOfSelectedTime = (string)timeComboBox.SelectedItem;
            string[] separator = { ":", };
            string[] atributesOfSelectedTime = elementsOfSelectedTime.Split(separator, StringSplitOptions.None);
            DateTime selectedDate = (DateTime)datePicker.SelectedDate;
            return new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 
                int.Parse(atributesOfSelectedTime[0]), int.Parse(atributesOfSelectedTime[1]), 00);
        }
        private void CreateThreadForMovingEquipment(DateTime dateForMovingEquipment)
        {
            Thread tr = new Thread(() =>
            {
                while (true)
                {
                    DateTime dateForCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                    if(dateForMovingEquipment.Equals(dateForCompare))
                    {
                        currentRoom.EquipmentInRoom.ChangeEquipmentState(quantityForMove, idOfSelectedEquipment);
                        nextRoom.EquipmentInRoom.AcceptEquipmentFromOtherRoom(quantityForMove, idOfSelectedEquipment);
                        break;
                    }
                }
            });

            tr.Start();
        }
        private void LoadRoomComboBox()
        {
            potencialRoomForMove = new ObservableCollection<Room>(RoomController.GetInstance().GetRooms());
            potencialRoomForMove.Remove(currentRoom);
            nextRoomComboBox.ItemsSource = null;
            nextRoomComboBox.ItemsSource = potencialRoomForMove;
        }
        private void LoadTimeComboBox()
        {
            timeComboBox.ItemsSource = null;
            timeComboBox.ItemsSource = MainWindow.GetTimeList();
        }
    }
}
