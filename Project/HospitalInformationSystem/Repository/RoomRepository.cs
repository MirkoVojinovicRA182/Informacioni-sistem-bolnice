/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using HospitalInformationSystem.Controller;
using Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HospitalInformationSystem.Repository
{
    public class RoomRepository : IRepository
    {
        public void saveInFile()
        {
            FileStream fs = new FileStream("Rooms.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, RoomController.GetInstance().getRooms());
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
            if (File.Exists("Rooms.dat"))
            {
                FileStream fs = new FileStream("Rooms.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    RoomController.GetInstance().setRooms((List<Room>)formatter.Deserialize(fs));
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