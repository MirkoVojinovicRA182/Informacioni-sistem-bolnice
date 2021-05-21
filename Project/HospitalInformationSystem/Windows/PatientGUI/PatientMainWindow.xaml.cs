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
        private Patient loggedInPatient;
        private static PatientMainWindow instance = null;
        private List<DispatcherTimer> notificationTimers = new List<DispatcherTimer>();
        public PatientMainWindow(Patient patient)
        {
            InitializeComponent();
            LoadDataFromFiles();
            loggedInPatient = patient;
            Notify();
        }
 
        public static PatientMainWindow GetInstance(Patient patient)
        {
            if (instance == null)
                instance = new PatientMainWindow(patient);
            return instance;
        }

        private void LoadDataFromFiles()
        {
            DoctorController.getInstance().LoadFromFile();
            PatientController.getInstance().LoadFromFile();
            AppointmentController.getInstance().loadFromFile();
        }

        private void AppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAppointmentCRUDOperationsWindow window = new PatientAppointmentCRUDOperationsWindow(loggedInPatient);
            window.Show();
            this.Hide();
        }

        private void TherapiesButton_Click(object sender, RoutedEventArgs e)
        {
            PatientTherapiesWindow window = new PatientTherapiesWindow(loggedInPatient);
            window.Show();
            this.Hide();
        }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationsWindow window = new NotificationsWindow(loggedInPatient);
            window.Show();
            this.Hide();
        }
        private void HospitalReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (HospitalReviewValidation())
            { 
                ReviewHospitalWindow window = new ReviewHospitalWindow(loggedInPatient);
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
            return loggedInPatient.Activity.NumberOfFinishedAppointmentsSinceReview >= 5;
        }

        private void CalculateFinishedAppointments()
        {
            foreach (var appointment in AppointmentController.getInstance().GetAppointmentsByPatient(loggedInPatient))
            {
                if (AppointmentIsFinished(appointment) && appointment.StartTime.AddMinutes(30).CompareTo(loggedInPatient.Activity.HospitalReviewTime) > 0)
                {
                    loggedInPatient.Activity.NumberOfFinishedAppointmentsSinceReview++;
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
            PatientAnamnesisWindow window = new PatientAnamnesisWindow(loggedInPatient);
            window.Show();
        }
        public void Notify()
        {
            TimeSpan dayTime = new TimeSpan(24, 00, 00);
            TimeSpan currentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
            List<TimeSpan> notificationTime = new List<TimeSpan>();
            List<Notification> notifications = NotificationController.GetInstance().GetNotifications();

            for (int i = 0; i < notifications.Count; i++)
            {
                if (notifications[i].StartDate.CompareTo(DateTime.Now) < 0 && notifications[i].EndDate.CompareTo(DateTime.Now) > 0 && notifications[i].IsEnabled == true)
                {
                    notificationTime.Add(notifications[i].Time.TimeOfDay);
                }
            }

            for (int i = 0; i < notificationTime.Count; i++)
            {
                TimeSpan timeToNotification = ((dayTime - currentTime) + notificationTime[i]);
                if (timeToNotification.TotalHours > 24)
                    timeToNotification -= new TimeSpan(24, 0, 0);
                notificationTimers.Add(new System.Windows.Threading.DispatcherTimer());
                notificationTimers[i].Tick += new EventHandler(DispatcherTimer_Tick);
                notificationTimers[i].Interval = timeToNotification;
                notificationTimers[i].Start();
            }

        }

        public void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<Notification> notifications = NotificationController.GetInstance().GetNotificationsByPatient(loggedInPatient);
            CustomNotificationWindow window = new CustomNotificationWindow();
            for (int i = 0; i < notifications.Count; i++)
            {
                if (DateTime.Now.TimeOfDay.CompareTo(notifications[i].Time.AddMinutes(-1).TimeOfDay) > 0 && DateTime.Now.TimeOfDay.CompareTo(notifications[i].Time.AddMinutes(1).TimeOfDay) < 0)
                {
                    window.notificationTextBlock.Text = notifications[i].Contents;
                    window.ShowDialog();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoctorController.getInstance().SaveInFlie();
            AppointmentController.getInstance().saveInFile();
            PatientController.getInstance().SaveInFile();
            instance = null;
        }
    }
}
