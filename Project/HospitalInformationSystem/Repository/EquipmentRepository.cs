using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    class EquipmentRepository
    {
        public void saveInFile()
        {
            FileStream fs = new FileStream("Equipment.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, EquipmentController.getInstance().getEquipment());
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
            if (File.Exists("Equipment.dat"))
            {
                FileStream fs = new FileStream("Equipment.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    EquipmentController.getInstance().setEquipment((List<Equipment>)formatter.Deserialize(fs));
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
