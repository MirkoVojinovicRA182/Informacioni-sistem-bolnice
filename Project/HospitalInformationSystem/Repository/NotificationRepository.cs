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
        public void saveInFile()
        {
            // TODO: implement
            FileStream fs = new FileStream("Notifications.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, NotificationController.GetInstance().GetNotifications());
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
    }
}
