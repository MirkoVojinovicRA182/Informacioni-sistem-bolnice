using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using Model;
using System;
using System.Collections;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class RoomRenovationWindow : Window
    {
        private Room roomSelectedFromTable;
        private Room roomSelectedFromComboBox;
        private DateTime startTermDate;
        private DateTime endTermDate;
        private string[] selectedTimeSeparator = { ":", };
        private string nameOfNewRoom;
        private static RoomRenovationWindow instance;
        public static RoomRenovationWindow GetInstance(Room roomForRenovation)
        {
            if (instance == null)
                instance = new RoomRenovationWindow(roomForRenovation);
            return instance;
        }
        private RoomRenovationWindow(Room roomForRenovation)
        {
            InitializeComponent();
            this.roomSelectedFromTable = roomForRenovation;
            roomForMergeComboBox.ItemsSource = RoomController.GetInstance().GetRooms();
            LoadTimeComboBoxes();
        }
        private void LoadTimeComboBoxes()
        {
            startTimeComboBox.ItemsSource = MainWindow.GetTimeList();
            endTimeComboBox.ItemsSource = MainWindow.GetTimeList();
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            MakeStartAndEndTermDate();
            if (CheckTheCorrectnessOfTheTerm())
            {
                CheckWindowOptionalControlsSelection();
                GiveFeedbackToManager();
                this.Close();
            }
            else
                MessageBox.Show("Nije moguće zakazati renoviranje u izabranom terminu jer je tada zauzeta.", "Greška", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
        private void MakeStartAndEndTermDate()
        {
            startTermDate = GetDate((string)startTimeComboBox.SelectedItem, startDatePicker);
            endTermDate = GetDate((string)endTimeComboBox.SelectedItem, endDatePicker);
        }
        private DateTime GetDate(string selectedTime, DatePicker datePickerWpfControl)
        {
            string[] elementsOfSelectedTime = selectedTime.Split(selectedTimeSeparator, StringSplitOptions.None);
            return new DateTime(datePickerWpfControl.SelectedDate.Value.Year, datePickerWpfControl.SelectedDate.Value.Month, datePickerWpfControl.SelectedDate.Value.Day,
                int.Parse(elementsOfSelectedTime[0]), int.Parse(elementsOfSelectedTime[1]), 00);
        }
        private bool CheckTheCorrectnessOfTheTerm()
        {
            foreach(Appointment appointment in RoomController.GetInstance().GetAppointmentsInRoom(roomSelectedFromTable.Name))
            {
                if (AppointmentStartIsBetweenTermTimeSpan(appointment) || TermIsInAppointmentTimeSpan(appointment) ||
                    AppointmentEndIsBetweenTermTimeSpan(appointment))
                    return false;
            }
            return true;
        }
        private bool AppointmentStartIsBetweenTermTimeSpan(Appointment appointment)
        {
            return appointment.StartTime >= startTermDate && appointment.StartTime <= endTermDate;
        }
        private bool TermIsInAppointmentTimeSpan(Appointment appointment)
        {
            return startTermDate >= appointment.StartTime && startTermDate <= appointment.StartTime.AddMinutes(30);
        }
        private bool AppointmentEndIsBetweenTermTimeSpan(Appointment appointment)
        {
            return appointment.StartTime.AddMinutes(30) >= startTermDate && appointment.StartTime.AddMinutes(30) <= endTermDate;
        }
        private RoomRenovationState NewRoomRenovationState()
        {
            return new RoomRenovationState(GetDate((string)startTimeComboBox.SelectedItem, startDatePicker), GetDate((string)endTimeComboBox.SelectedItem, endDatePicker));
        }
        private void CheckWindowOptionalControlsSelection()
        {
            if ((bool)duplicateRoomCheckBox.IsChecked)
            {
                StartRoomDuplicate();
            }
            else if (roomForMergeComboBox.SelectedItem != null)
            {
                StartRoomsMerge();
            }
            else
                StartCommonRenovation();

        }
        private void StartRoomDuplicate()
        {
            roomSelectedFromTable.RoomRenovationState = NewRoomRenovationState();
            CreateThreadForChangeRoomActivityStatus(roomSelectedFromTable);
            CreateThreadForDuplicatingRoom();
        }
        private void StartRoomsMerge()
        {
            GetRoomForMergeAndNewRoomName();
            roomSelectedFromTable.RoomRenovationState = NewRoomRenovationState();
            Room roomForMerge = (Room)roomForMergeComboBox.SelectedItem;
            roomForMerge.RoomRenovationState = NewRoomRenovationState();
            CreateThreadForChangeRoomActivityStatus(roomSelectedFromTable);
            CreateThreadForChangeRoomActivityStatus((Room)roomForMergeComboBox.SelectedItem);
            CreateThreadForRoomMerge();
        }
        private void GetRoomForMergeAndNewRoomName()
        {
            nameOfNewRoom = newMergedRoomTextBox.Text;
            roomSelectedFromComboBox = (Room)roomForMergeComboBox.SelectedItem;
        }
        private void StartCommonRenovation()
        {
            CreateThreadForChangeRoomActivityStatus(roomSelectedFromTable);
        }
        private void CreateThreadForChangeRoomActivityStatus(Room roomForRenovation)
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    roomForRenovation.RoomRenovationState.ActivityStatus = DateTime.Now >= roomForRenovation.RoomRenovationState.StartDate 
                    && DateTime.Now <= roomForRenovation.RoomRenovationState.EndDate;
                    if (RenovationIsComplete())
                        break;
                }
            });
            thread.Start();
        }
        private void CreateThreadForDuplicatingRoom()
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    if (RenovationIsComplete())
                    {
                        DeleteOneAndCreateTwoRooms();
                        break;
                    }
                }
            });
            thread.Start();
        }
        private void DeleteOneAndCreateTwoRooms()
        {
            RoomController.GetInstance().AddRoomToRoomList(new Room(RoomController.GetInstance().GetRooms().Count, roomSelectedFromTable.Name + "A", roomSelectedFromTable.Floor, roomSelectedFromTable.Type, new Hashtable()));
            RoomController.GetInstance().AddRoomToRoomList(new Room(RoomController.GetInstance().GetRooms().Count, roomSelectedFromTable.Name + "B", roomSelectedFromTable.Floor, roomSelectedFromTable.Type, new Hashtable()));
            RoomController.GetInstance().DeleteRoom(roomSelectedFromTable);
        }
        private void CreateThreadForRoomMerge()
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    if (RenovationIsComplete())
                    {
                        MergeRooms();
                        break;
                    }
                }
            });
            thread.Start();
        }
        private void MergeRooms()
        {
            RoomController.GetInstance().AddRoomToRoomList(new Room(roomSelectedFromTable.Id, nameOfNewRoom, roomSelectedFromTable.Floor, roomSelectedFromTable.Type, new Hashtable()));
            RoomController.GetInstance().DeleteRoom(roomSelectedFromTable);
            RoomController.GetInstance().DeleteRoom(roomSelectedFromComboBox);
        }
        private bool RenovationIsComplete()
        {
            return !roomSelectedFromTable.RoomRenovationState.ActivityStatus && DateTime.Now >= endTermDate;
        }
        private void GiveFeedbackToManager()
        {
            this.Close();
            MessageBox.Show("Uspešno ste zakazali renoviranje prostorije.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ComboBoxControlsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckIfTheTermIsSelected();
        }
        private void DatePickerControlsLayoutUpdated(object sender, EventArgs e)
        {
            CheckIfTheTermIsSelected();
        }
        private void CheckIfTheTermIsSelected()
        {
            confirmButton.IsEnabled = (startTimeComboBox.SelectedItem != null && endTimeComboBox.SelectedItem != null && startDatePicker.SelectedDate != null && endDatePicker.SelectedDate != null);
        }
        private void duplicateRoomCheckBox_LayoutUpdated(object sender, EventArgs e)
        {
            roomForMergeComboBox.IsEnabled = !(bool)duplicateRoomCheckBox.IsChecked;
            newMergedRoomTextBox.IsEnabled = !(bool)duplicateRoomCheckBox.IsChecked;
        }
        private void roomForMergeComboBox_LayoutUpdated(object sender, EventArgs e)
        {
            duplicateRoomCheckBox.IsEnabled = roomForMergeComboBox.SelectedItem == null;
        }
        private void cancelSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            roomForMergeComboBox.SelectedIndex = -1;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
