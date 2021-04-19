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
        public bool SaveInFile(List<Equipment> equipment)
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

        public bool LoadFromFile()
        {
            FileStream fs = new FileStream("Rooms.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                RoomDataBase.getInstance().setRoom((List<Room>)formatter.Deserialize(fs));
            }
            catch (SerializationException e)
            {
                throw;
            }
            finally
            {
                fs.Close();
            }

            return false;
        }
    }
}
