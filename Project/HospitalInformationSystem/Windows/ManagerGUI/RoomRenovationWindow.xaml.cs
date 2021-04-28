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
            CreateThreadForRenovationSimulation(MakeStartDate(), MakeEndDate());
            GiveFeedbackToManager();
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
                    if (DateTime.Now >= startDate && DateTime.Now <= endDate)
                        selectedRoom.IsInRenovationState = 1;
                    else
                        selectedRoom.IsInRenovationState = 0;
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

    }
}
