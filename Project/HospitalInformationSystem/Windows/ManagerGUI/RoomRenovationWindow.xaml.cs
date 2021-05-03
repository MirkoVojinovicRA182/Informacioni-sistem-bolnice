using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for RoomRenovationWindow.xaml
    /// </summary>
    public partial class RoomRenovationWindow : Window
    {
        private Room roomForRenovation;
        private DateTime startTermDate;
        private DateTime endTermDate;
        string[] selectedTimeSeparator = { ":", };
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
            this.roomForRenovation = roomForRenovation;
            LoadTimeComboBoxes();
        }
        private void LoadTimeComboBoxes()
        {
            startTimeComboBox.ItemsSource = GetTimeList();
            endTimeComboBox.ItemsSource = GetTimeList();
        }
        private List<String> GetTimeList()
        {
            List<String> timeList = new List<String>();
            foreach(string hour in GetHourList())
            {
                foreach(string minute in GetMinuteList())
                {
                    timeList.Add(hour + ":" + minute);
                }
            }
            return timeList;
        }
        private List<String> GetHourList()
        {
            List<String> hourList = new List<String>();
            for(int i = 6; i <= 21; i++)
            {
                hourList.Add(GetHour(i));
            }
            return hourList;
        }
        private string GetHour(int currentHour)
        {
            if (currentHour >= 6 && currentHour <= 9)
                return "0" + currentHour.ToString();
            else
                return currentHour.ToString();
        }
        private List<String> GetMinuteList()
        {
            List<String> minuteList = new List<String>();
            for (int i = 0; i <= 59; i++)
            {
                minuteList.Add(GetMinute(i));
            }
            return minuteList;
        }
        private string GetMinute(int currentMinute)
        {
            if (currentMinute >= 0 && currentMinute <= 9)
                return "0" + currentMinute.ToString();
            else
                return currentMinute.ToString();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTheCorrectnessOfTheTerm())
            {
                RoomController.getInstance().SetRenovationStateToRoom(roomForRenovation, CreateRoomRenovationState());
                CreateThreadForRenovationSimulation(startTermDate, endTermDate);
                GiveFeedbackToManager();
            }
            else
                MessageBox.Show("Nije moguće zakazati renoviranje u izabranom terminu jer je tada zauzeta.", "Greška", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
        private RoomRenovationState CreateRoomRenovationState()
        {
            return new RoomRenovationState(GetDate((string)startTimeComboBox.SelectedItem, startDatePicker), GetDate((string)endTimeComboBox.SelectedItem, endDatePicker));
        }
        private DateTime GetDate(string selectedTime, DatePicker selectedDate)
        {
            string[] elementsOfSelectedTime = selectedTime.Split(selectedTimeSeparator, StringSplitOptions.None);
            return new DateTime(selectedDate.SelectedDate.Value.Year, selectedDate.SelectedDate.Value.Month, selectedDate.SelectedDate.Value.Day,
                int.Parse(elementsOfSelectedTime[0]), int.Parse(elementsOfSelectedTime[1]), 00);
        }
        private bool CheckTheCorrectnessOfTheTerm()
        {
            foreach (Appointment currentAppointment in AppointmentController.getInstance().getAppointment())
            {
                if (Room.Equals(currentAppointment.room, roomForRenovation))
                {
                    if (currentAppointment.StartTime >= startTermDate && currentAppointment.StartTime <= endTermDate)
                        return false;
                }
            }
            return true;
        }
        private void CreateThreadForRenovationSimulation(DateTime startDate, DateTime endDate)
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    RoomController.getInstance().CheckRenovationTerm(roomForRenovation);
                }
            });
            thread.Start();
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
            if (startTimeComboBox.SelectedItem != null && endTimeComboBox.SelectedItem != null && startDatePicker.SelectedDate != null && endDatePicker.SelectedDate != null)
                confirmButton.IsEnabled = true;
            else
                confirmButton.IsEnabled = false;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
