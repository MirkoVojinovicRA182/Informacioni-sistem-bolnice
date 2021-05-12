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

        private List<Room> _allRooms;
        private RoomRepository _repository;
        public RoomService()
        {
            _allRooms = new List<Room>();
            _repository = new RoomRepository();
        }

        public void SaveRoomsInFile()
        {
            _repository.saveInFile();
        }

        public void LoadRoomsFromFile()
        {
            _repository.loadFromFile();
        }
        public void CreateRoom(Room newRoom)
        {
            _allRooms.Add(newRoom);
        }

        public void DeleteRoom(Room room)
        {
            _allRooms.Remove(room);
            List<Appointment> list = AppointmentController.getInstance().findAppointmentByRoom(room);
            foreach(Appointment appointment in list)
            {
                AppointmentController.getInstance().removeAppointment(appointment);
            }
        }
        public void ChangeRoom(Room room, int newId, string newName, TypeOfRoom newType, int newFloor)
        {
            room.Id = newId;
            room.Name = newName;
            room.Type = newType;
            room.Floor = newFloor;
        }

        public void SetRoomEquipment(Room room, Hashtable eq)
        {
            if (room.Equipment == null)
                room.Equipment = new Hashtable();
            room.Equipment.Clear();
            foreach (DictionaryEntry de in eq)
                room.Equipment.Add(de.Key, de.Value);
        }
        public void DeleteEquipment(string id)
        {
            foreach(Room room in _allRooms)
            {
                if (room.Equipment.Contains(id))
                    room.Equipment.Remove(id);
            }
        }
        public List<Room> GetRooms()
        {
            return _allRooms;
        }
        public void SetRooms(List<Room> rooms)
        {
            _allRooms.Clear();

            foreach (Room room in rooms)
                _allRooms.Add(room);
        }
        public void ChangeStaticEquipmentState(Room room, int currentQuantity, int moveQuantity, string key)
        {
            room.Equipment[key] = currentQuantity - moveQuantity;

            if ((currentQuantity - moveQuantity) == 0)
                room.Equipment.Remove(key);
            
        }
        public void MoveStaticEqToNextRoom(Room room, int moveQuantity, string key)
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
        public bool FindRoom(int id)
        {
            foreach (Room room in _allRooms)
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
            foreach(Room room in _allRooms)
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
            _allRooms.Add(newRoom);
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
    }
}