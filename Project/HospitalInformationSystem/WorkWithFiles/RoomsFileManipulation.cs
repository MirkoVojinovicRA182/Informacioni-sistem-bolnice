/***********************************************************************
 * Module:  RoomsFileManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Class WorkWithFiles.RoomsFileManipulation
 ***********************************************************************/

using Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WorkWithFiles
{
    public class RoomsFileManipulation : IFileManipulation
    {
        public bool SaveInFile()
        {
            FileStream fs = new FileStream("Rooms.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, RoomDataBase.getInstance().getRooms());
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
            //if (File.Exists("Rooms.dat"))
            //{
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
            //}

            return false;
        }

    }
}