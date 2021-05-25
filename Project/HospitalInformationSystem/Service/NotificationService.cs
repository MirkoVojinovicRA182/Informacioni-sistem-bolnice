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

namespace HospitalInformationSystem.Service
{
    public class NotificationService
    {
        private NotificationRepository _repository;
        public NotificationService()
        {
            _repository = new NotificationRepository();
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
    }
}
