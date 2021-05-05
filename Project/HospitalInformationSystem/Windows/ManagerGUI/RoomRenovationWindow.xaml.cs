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
            startTimeComboBox.ItemsSource = MainWindow.GetTimeList();
            endTimeComboBox.ItemsSource = MainWindow.GetTimeList();
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            MakeStartAndEndTermDate();
            if (CheckTheCorrectnessOfTheTerm())
            {
                RoomController.getInstance().SetRenovationStateToRoom(roomForRenovation, NewRoomRenovationState());
                CreateThreadForRenovationSimulation();
                GiveFeedbackToManager();
            }
            else
                MessageBox.Show("Nije moguće zakazati renoviranje u izabranom terminu jer je tada zauzeta.", "Greška", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
        private bool CheckTheCorrectnessOfTheTerm()
        {
            foreach(Appointment appointment in RoomController.getInstance().GetAppointmentsInRoom(roomForRenovation.Name))
            {
                if (AppointmentStartIsBetweenTermTimeSpan(appointment) || TermIsInAppointmentTimeSpan(appointment) ||
                    AppointmentEndIsBetweenTermTimeSpan(appointment))
                    return false;
            }
            return true;
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
        private void CreateThreadForRenovationSimulation()
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
            confirmButton.IsEnabled = (startTimeComboBox.SelectedItem != null && endTimeComboBox.SelectedItem != null && startDatePicker.SelectedDate != null && endDatePicker.SelectedDate != null);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
