using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using Model;
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

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for CustomNotificationsWindow.xaml
    /// </summary>
    public partial class CustomNotificationsWindow : Window
    {
        public Patient loggedInPatient;
        public CustomNotificationsWindow(Patient patient)
        {
            InitializeComponent();
            LoadHourComboBox();
            LoadMinuteComboBox();
            LoadNotificationComboBox();
            loggedInPatient = patient;
        }

        private void NewNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            Notification newNotification = new Notification(notificationTextBox.Text, GetTimeFromComboBoxes(), (DateTime)startDatePicker.SelectedDate, (DateTime)endDatePicker.SelectedDate, (bool)notificationCheckBox.IsChecked);
            newNotification.Patient = loggedInPatient;
            NotificationController.GetInstance().AddNotification(newNotification);
            LoadNotificationComboBox();
            PatientMainWindow.GetInstance(loggedInPatient).Notify();
        }

        private void NotificationComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Notification notification = (Notification)notificationComboBox.SelectedItem;
            SetSelectedValues(notification);
        }

        private void SetSelectedValues(Notification notification)
        {
            if (notificationComboBox.SelectedItem != null)
            {
                SetTimeComboBoxes(notification);
                SetDates(notification);
                notificationTextBox.Text = notification.Contents;
                notificationCheckBox.IsChecked = notification.IsEnabled;
            }
        }

        private void SetDates(Notification notification)
        {
            startDatePicker.SelectedDate = notification.StartDate;
            endDatePicker.SelectedDate = notification.EndDate;
        }

        private void SetTimeComboBoxes(Notification notification)
        {
            string time = notification.Time.ToString();
            string[] timeArray = time.Split(':');
            string[] hourArray = timeArray[0].Split(' ');
            hourComboBox.SelectedItem = Int32.Parse(hourArray[1]);
            minuteComboBox.SelectedItem = Int32.Parse(timeArray[1]);
        }

        private void NotificationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Notification notification = (Notification)notificationComboBox.SelectedItem;
            SetSelectedValues(notification);
        }

        private void LoadNotificationComboBox()
        {
            notificationComboBox.ItemsSource = NotificationController.GetInstance().GetNotifications();
        }

        private void LoadHourComboBox()
        {
            hourComboBox.ItemsSource = Enumerable.Range(0, 24);
        }

        private void LoadMinuteComboBox()
        {
            minuteComboBox.ItemsSource = Enumerable.Range(0, 60).ToList<int>();
        }

        private DateTime GetTimeFromComboBoxes()
        {
            return DateTime.Parse(hourComboBox.SelectedItem + ":" + minuteComboBox.SelectedItem);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(loggedInPatient).Show();
        }

        private void DeleteNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationController.GetInstance().RemoveNotification((Notification)notificationComboBox.SelectedItem);
            PatientMainWindow.GetInstance(loggedInPatient).Notify();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            Notification newNotification = new Notification(notificationTextBox.Text, GetTimeFromComboBoxes(), (DateTime)startDatePicker.SelectedDate, (DateTime)endDatePicker.SelectedDate, (bool)notificationCheckBox.IsChecked);
            NotificationController.GetInstance().ChangeNotification((Notification)notificationComboBox.SelectedItem, newNotification);
            PatientMainWindow.GetInstance(loggedInPatient).Notify();
        }
    }
}
