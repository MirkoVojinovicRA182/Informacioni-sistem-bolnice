/***********************************************************************
 * Module:  RoomService.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Service.RoomService
 ***********************************************************************/

using Model;
using HospitalInformationSystem.Windows;
using HospitalInformationSystem.Service;
using System.Collections.Generic;
using System.Collections;
using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Repository;
using HospitalInformationSystem.Model;
using System;

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
            roomR.saveInFile();
        }

        public void loadFromFile()
        {
            roomR.loadFromFile();
        }
        public void createRoom(int floor, int id, string name, TypeOfRoom type, Hashtable equipment)
        {
            // TODO: implement
            Room newRoom = new Room(id, name, floor, type);
            setRoomEquipment(newRoom, equipment);
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
            if (room.Equipment == null)
                room.Equipment = new Hashtable();
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
        public bool findRoom(int id)
        {
            foreach (Room room in roomList)
            {
                if (room.Id == id)
                    return true;
            }
            return false;
        }

        public void SetRenovationStateToRoom(Room room, RoomRenovationState roomRenovationState)
        {
            room.RoomRenovationState = roomRenovationState;
        }
        public void CheckRenovationTerm(Room roomForRenovation)
        {
            if (DateTime.Now >= roomForRenovation.RoomRenovationState.StartDate && DateTime.Now <= roomForRenovation.RoomRenovationState.EndDate)
                roomForRenovation.RoomRenovationState.ActivityStatus = true;
            else
                roomForRenovation.RoomRenovationState.ActivityStatus = false;
        }
        public Room GetMagacine()
        {
            foreach(Room room in roomList)
            {
                if (string.Equals(room.Name, "Magacin"))
                    return room;
            }
            return null;
        }
        public void AddEquipmentToMagacine(Equipment newEquipment)
        {
            GetMagacine().Equipment.Add(newEquipment.Id, newEquipment.Quantity);
        }
        public void ChangeEquipmentId(string id)
        {

        }
        public void AddRoomToRoomList(Room newRoom)
        {
            roomList.Add(newRoom);
        }
    }
}