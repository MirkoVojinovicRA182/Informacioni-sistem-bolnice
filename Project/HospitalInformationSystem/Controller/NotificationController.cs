using HospitalInformationSystem.Model;
using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Controller
{
    public class NotificationController
    {
        private NotificationService _notificationService;
        private static NotificationController _instance = null;
        public static NotificationController GetInstance()
        {
            if (_instance == null)
                _instance = new NotificationController();
            return _instance;
        }
        private NotificationController()
        {
            _notificationService = new NotificationService();
        }
        public List<Notification> GetNotifications()
        {
            return _notificationService.GetNotifications();
        }
        public void SetNotifications(List<Notification> newNotificationList)
        {
            _notificationService.SetNotifications(newNotificationList);
        }
        public void AddNotification(Notification notification)
        {
            _notificationService.AddNotification(notification);
        }
        public void RemoveAllNotifications()
        {
            _notificationService.RemoveAllNotifications();
        }
        public void RemoveNotification(Notification notificationForRemoval)
        {
            _notificationService.RemoveNotification(notificationForRemoval);
        }
        public void ChangeNotification(Notification oldNotification, Notification newNotification)
        {
            _notificationService.ChangeNotification(oldNotification, newNotification);
        }
        public List<Notification> GetNotificationsByPatient(Patient patient)
        {
            return _notificationService.GetNotificationsByPatient(patient);
        }
        public void SaveInFile()
        {
            _notificationService.SaveInFile();
        }
        public void LoadFromFile()
        {
            _notificationService.LoadFromFile();
        }
    }
}
