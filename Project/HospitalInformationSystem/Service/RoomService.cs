/***********************************************************************
 * Module:  RoomService.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Service.RoomService
 ***********************************************************************/

using Model;
using System.Collections.Generic;
using System.Collections;
using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Repository;

namespace HospitalInformationSystem.Service
{
    public class RoomService
    {

        List<Room> roomList;
        RoomRepository roomR;
        public RoomService()
        {
            roomList = new List<Room>();
            roomR = new RoomRepository();
        }

        public void saveInFile()
        {
            roomR.SaveInFile();
        }

        public void loadFromFile()
        {
            roomR.LoadFromFile();
        }
        public void createRoom(int floor, int id, string name, TypeOfRoom type, Hashtable equipment)
        {
            // TODO: implement
            Room newRoom = new Room(id, name, floor, type);
            newRoom.Equipment = equipment;

            roomList.Add(newRoom);

        }

        public void deleteRoom(Room room)
        {
            // TODO: implement

            roomList.Remove(room);

            //brisanje termina koji se odvijaju u datoj prostoriji
            List<Appointment> list = AppointmentController.getInstance().findAppointmentByRoom(room);

            foreach(Appointment appointment in list)
            {
                AppointmentController.getInstance().removeAppointment(appointment);
            }
        }

        public void changeRoom(Room room, int newId, string newName, TypeOfRoom newType, int newFloor)
        {
            // TODO: implement
            room.Id = newId;
            room.Name = newName;
            room.Type = newType;
            room.Floor = newFloor;

        }

        public void setRoomEquipment(Room room, Hashtable eq)
        {
            room.Equipment.Clear();
            foreach (DictionaryEntry de in eq)
                room.Equipment.Add(de.Key, de.Value);
        }

        public void deleteEquipment(string id)
        {
            //List<Room> rooms = RoomDataBase.getInstance().getRooms();
            foreach(Room room in roomList)
            {
                if (room.Equipment.Contains(id))
                    room.Equipment.Remove(id);
            }
        }

        public List<Room> getRooms()
        {
            //return RoomDataBase.getInstance().getRooms();

            return roomList;
        }

        public void setRooms(List<Room> rooms)
        {
            roomList.Clear();

            foreach (Room room in rooms)
                roomList.Add(room);
        }

        public void changeStaticEquipmentState(Room room, int currentQuantity, int moveQuantity, string key)
        {
            room.Equipment[key] = currentQuantity - moveQuantity;

            if ((currentQuantity - moveQuantity) == 0)
                room.Equipment.Remove(key);
            
        }

        public void moveStaticEqToNextRoom(Room room, int moveQuantity, string key)
        {
            if (room.Equipment.Contains(key))
            {
                int currentQuantity = (int)room.Equipment[key];
                room.Equipment[key] = currentQuantity + moveQuantity;
            }
            else
            {
                room.Equipment.Add(key, moveQuantity);
            }
        }

        public bool checkId(int id)
        {
            Room founded = findRoom(id);
            foreach(Room room in roomList)
            {
                if (room.Id == id && room.Id != founded.Id)
                    return true;
            }
            return false;
        }

        public Room findRoom(int id)
        {
            foreach (Room room in roomList)
            {
                if (room.Id == id)
                    return room;
            }
            return null;
        }
    }
}