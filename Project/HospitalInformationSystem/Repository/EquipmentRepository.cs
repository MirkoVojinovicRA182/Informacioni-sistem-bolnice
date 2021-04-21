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
        public bool saveInFile(List<Equipment> equipment)
        {
            FileStream fs = new FileStream("Equipment.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, equipment);
            }
            catch (SerializationException e)
            {

                throw;
            }
            finally
            {
                fs.Close();
            }

            return true;
        }

        public List<Equipment> loadFromFile()
        {
            List<Equipment> list = new List<Equipment>();
            if (File.Exists("Equipment.dat"))
            {
                FileStream fs = new FileStream("Equipment.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    list = ((List<Equipment>)formatter.Deserialize(fs));
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

            return list;
        }
    }
}
