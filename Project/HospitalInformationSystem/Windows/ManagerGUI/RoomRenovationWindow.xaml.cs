using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RoomRenovationWindow.xaml
    /// </summary>
    public partial class RoomRenovationWindow : Window
    {
        private Room selectedRoom;
        private static RoomRenovationWindow instance = null;
        private DateTime startDate;
        private DateTime endDate;
        public static RoomRenovationWindow GetInstance(Room selectedRoom)
        {
            if (instance == null)
                instance = new RoomRenovationWindow(selectedRoom);
            return instance;
        }
        private RoomRenovationWindow(Room selectedRoom)
        {
            InitializeComponent();
            this.selectedRoom = selectedRoom;
            LoadTimeComboBoxes();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            MakeTerm();
            if (CheckTheCorrectnessOfTheTerm())
            {
                RoomController.getInstance().SetRenovationStateToRoom(selectedRoom, new RoomRenovationState(startDate, endDate));
                CreateThreadForRenovationSimulation(startDate, endDate);
                GiveFeedbackToManager();
            }
            else
                MessageBox.Show("Nije moguće zakazati renoviranje u izabranom terminu jer je tada zauzeta.", "Greška", MessageBoxButton.OK, MessageBoxImage.Hand);

        }
        private void MakeTerm()
        {
            startDate = MakeStartDate();
            endDate = MakeEndDate();
        }

        private DateTime MakeStartDate()
        {
            string selectedStartTime = (string)startTimeComboBox.SelectedItem;
            string[] separator1 = { ":", };
            string[] atributesOfSelectedStartTime = selectedStartTime.Split(separator1, StringSplitOptions.None);
            DateTime selectedStartDate = (DateTime)startDatePicker.SelectedDate;
            return new DateTime(selectedStartDate.Year, selectedStartDate.Month, selectedStartDate.Day,
                int.Parse(atributesOfSelectedStartTime[0]), int.Parse(atributesOfSelectedStartTime[1]), 00);
        }

        private DateTime MakeEndDate()
        {
            string selectedEndTime = (string)endTimeComboBox.SelectedItem;
            string[] separator2 = { ":", };
            string[] atributesOfSelectedEndTime = selectedEndTime.Split(separator2, StringSplitOptions.None);
            DateTime selectedEndDate = (DateTime)endDatePicker.SelectedDate;
            return new DateTime(selectedEndDate.Year, selectedEndDate.Month, selectedEndDate.Day,
                int.Parse(atributesOfSelectedEndTime[0]), int.Parse(atributesOfSelectedEndTime[1]), 00);
        }
        private void CreateThreadForRenovationSimulation(DateTime startDate, DateTime endDate)
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    RoomController.getInstance().CheckRenovationTerm(selectedRoom);
                }
            });
            thread.Start();
        }
        private void LoadTimeComboBoxes()
        {
            startTimeComboBox.ItemsSource = GetAllTimes();
            endTimeComboBox.ItemsSource = GetAllTimes();
        }

        private List<String> GetAllTimes()
        {
            List<String> timesList = new List<String>();
            for (int i = 6; i <= 21; i++)
            {
                for (int j = 0; j <= 59; j++)
                {
                    timesList.Add(i.ToString() + ":" + j.ToString());
                }
            }
            return timesList;
        }
        private void GiveFeedbackToManager()
        {
            this.Close();
        }
        private bool CheckTheCorrectnessOfTheTerm()
        {
            foreach (Appointment app in AppointmentController.getInstance().getAppointment())
            {
                if (Room.Equals(app.room, selectedRoom))
                {
                    if (app.StartTime >= startDate && app.StartTime <= endDate)
                        return false;
                }
            }

            return true;
        }

    }
}
