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
using System.Windows.Threading;

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {
        private Patient _loggedInPatient;
        private static PatientMainWindow _instance = null;
        private List<DispatcherTimer> _notificationTimers = new List<DispatcherTimer>();
        public PatientMainWindow(Patient patient)
        {
            InitializeComponent();
            LoadDataFromFiles();
            _loggedInPatient = patient;
            Notify();
        }
        public static PatientMainWindow GetInstance(Patient patient)
        {
            if (_instance == null)
                _instance = new PatientMainWindow(patient);
            return _instance;
        }
        private void LoadDataFromFiles()
        {
            DoctorController.getInstance().LoadFromFile();
            PatientController.getInstance().LoadFromFile();
            AppointmentController.getInstance().LoadAppointmentsFromFile();
            NotificationController.GetInstance().LoadFromFile();
        }
        private void AppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAppointmentCRUDOperationsWindow window = new PatientAppointmentCRUDOperationsWindow(_loggedInPatient);
            window.Show();
            this.Hide();
        }
        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationsWindow window = new NotificationsWindow(_loggedInPatient);
            window.Show();
            this.Hide();
        }
        private void HospitalReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (HospitalReviewValidation())
            { 
                ReviewHospitalWindow window = new ReviewHospitalWindow(_loggedInPatient);
                window.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Niste imali dovoljno pregleda od prethodnog puta kada ste ocenjivali bolnicu da bi ste je ponovo ocenjivali.", "Ocenjivanje bolnice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private bool HospitalReviewValidation()
        {
            CalculateFinishedAppointments();
            return _loggedInPatient.Activity.NumberOfFinishedAppointmentsSinceReview >= 5;
        }
        private void CalculateFinishedAppointments()
        {
            foreach (var appointment in AppointmentController.getInstance().GetAppointmentsByPatient(_loggedInPatient))
            {
                if (AppointmentIsFinished(appointment) && appointment.StartTime.AddMinutes(30).CompareTo(_loggedInPatient.Activity.HospitalReviewTime) > 0)
                {
                    _loggedInPatient.Activity.NumberOfFinishedAppointmentsSinceReview++;
                }
            }
        }
        private static bool AppointmentIsFinished(Appointment appointment)
        {
            return DateTime.Now.CompareTo(appointment.StartTime.AddMinutes(30)) > 0;
        }
        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AnamnesisButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAnamnesisWindow window = new PatientAnamnesisWindow(_loggedInPatient);
            window.Show();
        }
        public void Notify()
        {
            TimeSpan dayTime = new TimeSpan(24, 00, 00);
            TimeSpan currentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
            List<TimeSpan> notificationTime = new List<TimeSpan>();
            List<Notification> notifications = NotificationController.GetInstance().GetNotifications();

            GetEnabledNotificationTimes(notificationTime, notifications);

            CreateNotificationTimers(dayTime, currentTime, notificationTime);
        }
        private void CreateNotificationTimers(TimeSpan dayTime, TimeSpan currentTime, List<TimeSpan> notificationTime)
        {
            for (int i = 0; i < notificationTime.Count; i++)
            {
                TimeSpan timeToNotification = ((dayTime - currentTime) + notificationTime[i]);
                if (timeToNotification.TotalHours > 24)
                    timeToNotification -= new TimeSpan(24, 0, 0);
                _notificationTimers.Add(new System.Windows.Threading.DispatcherTimer());
                _notificationTimers[i].Tick += new EventHandler(DispatcherTimer_Tick);
                _notificationTimers[i].Interval = timeToNotification;
                _notificationTimers[i].Start();
            }
        }
        private static void GetEnabledNotificationTimes(List<TimeSpan> notificationTime, List<Notification> notifications)
        {
            for (int i = 0; i < notifications.Count; i++)
            {
                if (NotificationIsEnabled(notifications[i]))
                {
                    notificationTime.Add(notifications[i].Time.TimeOfDay);
                }
            }
        }
        private static bool NotificationIsEnabled(Notification notification)
        {
            return notification.StartDate.CompareTo(DateTime.Now) < 0 && notification.EndDate.CompareTo(DateTime.Now) > 0 && notification.IsEnabled == true;
        }
        public void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<Notification> notifications = NotificationController.GetInstance().GetNotificationsByPatient(_loggedInPatient);
            CustomNotificationWindow window = new CustomNotificationWindow();
            for (int i = 0; i < notifications.Count; i++)
            {
                CreateAndShowNotification(notifications[i], window);
            }
        }
        private static void CreateAndShowNotification(Notification notification, CustomNotificationWindow window)
        {
            window.notificationTextBlock.Text = notification.Contents;
            window.ShowDialog();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoctorController.getInstance().SaveInFlie();
            AppointmentController.getInstance().SaveAppointmentsInFile();
            PatientController.getInstance().SaveInFile();
            NotificationController.GetInstance().SaveInFile();
            _instance = null;
        }
    }
}
