using HospitalInformationSystem.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using HospitalInformationSystem.Model;

namespace HospitalInformationSystem.Repository
{
    public class NotificationRepository
    {
        private List<Notification> _allNotifications;
        public NotificationRepository()
        {
            _allNotifications = new List<Notification>();
        }
        public void saveInFile()
        {
            if (_allNotifications.Count != 0)
            {
                FileStream fs = new FileStream("Notifications.dat", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, _allNotifications);
                }
                catch (SerializationException e)
                {

                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }
        public void loadFromFile()
        {
            if (File.Exists("Notifications.dat"))
            {
                FileStream fs = new FileStream("Notifications.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    NotificationController.GetInstance().SetNotifications((List<Notification>)formatter.Deserialize(fs));
                }
                catch (SerializationException e)
                {
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }
        public List<Notification> GetNotifications()
        {
            return _allNotifications;
        }
        public void SetNotifications(List<Notification> newNotificationList)
        {
            RemoveAllNotifications();
            foreach (Notification newNotification in newNotificationList)
                AddNotification(newNotification);
        }
        public void AddNotification(Notification notification)
        {
            _allNotifications.Add(notification);
        }
        public void RemoveAllNotifications()
        {
            if (_allNotifications != null)
                _allNotifications.Clear();
        }
        public void RemoveNotification(Notification notificationForRemoval)
        {
            if (notificationForRemoval == null)
                return;
            if (notificationForRemoval != null)
                if (this._allNotifications.Contains(notificationForRemoval))
                    this._allNotifications.Remove(notificationForRemoval);
        }
    }
}
