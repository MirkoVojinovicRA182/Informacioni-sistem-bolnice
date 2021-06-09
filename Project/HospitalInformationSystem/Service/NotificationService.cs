using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using HospitalInformationSystem.Repository;
using HospitalInformationSystem.Windows.PatientGUI;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HospitalInformationSystem.Service
{
    public class NotificationService
    {
        private NotificationRepository _repository;
        private List<DispatcherTimer> _notificationTimers;
        public NotificationService()
        {
            _repository = new NotificationRepository();
            _notificationTimers = new List<DispatcherTimer>();
        }
        public List<Notification> GetNotifications()
        {
            return _repository.GetNotifications();
        }
        public void SetNotifications(List<Notification> newNotificationList)
        {
            _repository.SetNotifications(newNotificationList);
        }
        public void AddNotification(Notification notification)
        {
            _repository.AddNotification(notification);
        }
        public List<Notification> GetNotificationsByPatient(Patient patient)
        {
            List<Notification> notificationsByPatient = new List<Notification>();

            foreach (var notification in GetNotifications())
            {
                if (Object.Equals(notification.Patient, patient))
                    notificationsByPatient.Add(notification);
            }

            return notificationsByPatient;
        }
        public void RemoveAllNotifications()
        {
            _repository.RemoveAllNotifications();
        }
        public void RemoveNotification(Notification notificationForRemoval)
        {
            _repository.RemoveNotification(notificationForRemoval);
        }
        public void ChangeNotification(Notification oldNotification, Notification newNotification)
        {
            oldNotification.Contents = newNotification.Contents;
            oldNotification.EndDate = newNotification.EndDate;
            oldNotification.IsEnabled = newNotification.IsEnabled;
            oldNotification.StartDate = newNotification.StartDate;
            oldNotification.Time = newNotification.Time;
        }
        public void SaveInFile()
        {
            _repository.saveInFile();
        }
        public void LoadFromFile()
        {
            _repository.loadFromFile();
        }
        public void CreateNotificationTimers(TimeSpan dayTime, TimeSpan currentTime, List<TimeSpan> notificationTime, Patient patient)
        {
            for (int i = 0; i < notificationTime.Count; i++)
            {
                TimeSpan timeToNotification = ((dayTime - currentTime) + notificationTime[i]);
                if (timeToNotification.TotalHours > 24)
                    timeToNotification -= new TimeSpan(24, 0, 0);
                _notificationTimers.Add(new System.Windows.Threading.DispatcherTimer());
                _notificationTimers[i].Tick += new EventHandler((sender, e) => DispatcherTimer_Tick(sender, e, patient));
                _notificationTimers[i].Interval = timeToNotification;
                _notificationTimers[i].Start();
            }
        }
        public void GetEnabledNotificationTimes(List<TimeSpan> notificationTime, List<Notification> notifications)
        {
            for (int i = 0; i < notifications.Count; i++)
            {
                if (NotificationIsEnabled(notifications[i]))
                {
                    notificationTime.Add(notifications[i].Time.TimeOfDay);
                }
            }
        }
        public bool NotificationIsEnabled(Notification notification)
        {
            return notification.StartDate.CompareTo(DateTime.Now) < 0 && notification.EndDate.CompareTo(DateTime.Now) > 0 && notification.IsEnabled == true;
        }
        public void DispatcherTimer_Tick(object sender, EventArgs e, Patient patient)
        {
            List<Notification> notifications = NotificationController.GetInstance().GetNotificationsByPatient(patient);
            CustomNotificationWindow window = new CustomNotificationWindow();
            for (int i = 0; i < notifications.Count; i++)
            {
                if (DateTime.Now.TimeOfDay.CompareTo(notifications[i].Time.AddSeconds(-1).TimeOfDay) > 0 && DateTime.Now.TimeOfDay.CompareTo(notifications[i].Time.AddSeconds(1).TimeOfDay) < 0)
                {
                    CreateAndShowNotification(notifications[i], window);
                }
            }
        }
        public void CreateAndShowNotification(Notification notification, CustomNotificationWindow window)
        {
            window.notificationTextBlock.Text = notification.Contents;
            window.ShowDialog();
        }
        public void Notify(Patient patient)
        {
            TimeSpan dayTime = new TimeSpan(24, 00, 00);
            TimeSpan currentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
            List<TimeSpan> notificationTime = new List<TimeSpan>();
            List<Notification> notifications = NotificationController.GetInstance().GetNotifications();

            GetEnabledNotificationTimes(notificationTime, notifications);

            CreateNotificationTimers(dayTime, currentTime, notificationTime, patient);
        }
    }
}
