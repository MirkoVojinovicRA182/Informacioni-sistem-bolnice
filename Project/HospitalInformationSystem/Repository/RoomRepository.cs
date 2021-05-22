/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HospitalInformationSystem.Repository
{
    public class RoomRepository : IRepository
    {
        private List<Room> _allRooms;
        public RoomRepository()
        {
            _allRooms = new List<Room>();
        }
        public void saveInFile()
        {
            FileStream fs = new FileStream("Rooms.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, _allRooms);
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
                    SetRooms((List<Room>)formatter.Deserialize(fs));
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
        public void AddRoomToRoomList(Room newRoom)
        {
            _allRooms.Add(newRoom);
        }
        public List<Room> GetRooms()
        {
            return _allRooms;
        }
        public void SetRooms(List<Room> newRooms)
        {
            _allRooms.Clear();
            foreach (Room room in newRooms)
                _allRooms.Add(room);
        }
        public bool RoomExists(int roomId)
        {
            foreach (Room room in _allRooms)
            {
                if (room.Id == roomId)
                    return true;
            }
            return false;
        }
        public Room GetMagacine()
        {
            return _allRooms[0];
        }
        public bool EquipmentExistInRoom(string id, Hashtable roomEq)
        {
            foreach (DictionaryEntry de in roomEq)
            {
                if (string.Compare(id, de.Key.ToString()) == 0)
                    return true;
            }
            return false;
        }

        public List<Appointment> GetAppointmentsInRoom(string nameOfRoom)
        {
            List<Appointment> appointmentsInRoom = new List<Appointment>();
            foreach (Appointment app in AppointmentController.getInstance().getAppointment())
            {
                if (string.Equals(app.room.Name, nameOfRoom))
                    appointmentsInRoom.Add(app);
            }
            return appointmentsInRoom;
        }
        public void DeleteRoom(Room room)
        {
            _allRooms.Remove(room);
        }
    }
}