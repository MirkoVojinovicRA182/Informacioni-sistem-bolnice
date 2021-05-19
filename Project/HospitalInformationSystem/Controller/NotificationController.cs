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
        private NotificationService notificationService;
        private static NotificationController instance = null;
        public static NotificationController GetInstance()
        {
            if (instance == null)
                instance = new NotificationController();
            return instance;
        }
        private NotificationController()
        {
            notificationService = new NotificationService();
        }

        public List<Notification> GetNotifications()
        {
            return notificationService.GetNotifications();
        }

        public void SetNotifications(List<Notification> newNotificationList)
        {
            notificationService.SetNotifications(newNotificationList);
        }

        public void AddNotification(Notification notification)
        {
            notificationService.AddNotification(notification);
        }

        public void RemoveAllNotifications()
        {
            notificationService.RemoveAllNotifications();
        }

        public void RemoveNotification(Notification notificationForRemoval)
        {
            notificationService.RemoveNotification(notificationForRemoval);
        }

        public void ChangeNotification(Notification oldNotification, Notification newNotification)
        {
            notificationService.ChangeNotification(oldNotification, newNotification);
        }
        public List<Notification> GetNotificationsByPatient(Patient patient)
        {
            return notificationService.GetNotificationsByPatient(patient);
        }

        public void SaveInFile()
        {
            notificationService.SaveInFile();
        }

        public void LoadFromFile()
        {
            notificationService.LoadFromFile();
        }

    }
}
