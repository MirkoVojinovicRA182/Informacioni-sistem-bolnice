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
        private RoomRepository _repository;
        public RoomService()
        {
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
        public void AddNewRoom(Room newRoom)
        {
            GetRooms().Add(newRoom);
        }
        public void DeleteRoom(Room room)
        {
            GetRooms().Remove(room);
        }
        /*public void ChangeRoom(Room roomForChange, Room roomDTO)
        {
            //prebaciti u room class
            roomForChange.Id = roomDTO.Id;
            roomForChange.Name = roomDTO.Name;
            roomForChange.Type = roomDTO.Type;
            roomForChange.Floor = roomDTO.Floor;
        }*/
        public void SetRoomEquipment(Room room, Hashtable newEquipment)
        {
            room.Equipment.Clear();
            foreach (DictionaryEntry de in newEquipment)
                room.Equipment.Add(de.Key, de.Value);
        }
        public List<Room> GetRooms()
        {
            return _repository.GetRooms();
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
                room.Equipment[key] = (int)room.Equipment[key] + moveQuantity;
            else
                room.Equipment.Add(key, moveQuantity);
        }
        public bool RoomExists(int roomId)
        {
            return _repository.RoomExists(roomId);
        }
        public void SetRenovationStateToRoom(Room room, RoomRenovationState roomRenovationState)
        {
            room.RoomRenovationState = roomRenovationState;
        }
        public void ChangeRoomActivityStatus(Room roomForRenovation)
        {
            roomForRenovation.RoomRenovationState.ActivityStatus = DateTime.Now >= roomForRenovation.RoomRenovationState.StartDate && 
                DateTime.Now <= roomForRenovation.RoomRenovationState.EndDate;
        }
        public Room GetMagacine()
        {
            return _repository.GetMagacine();
        }
        public void AddEquipmentToMagacine(Equipment newEquipment)
        {
            GetMagacine().Equipment.Add(newEquipment.Id, newEquipment.Quantity);
        }
        public void AddRoomToRoomList(Room newRoom)
        {
            GetRooms().Add(newRoom);
        }
        public List<Appointment> GetAppointmentsInRoom(string nameOfRoom)
        {
            return _repository.GetAppointmentsInRoom(nameOfRoom);
        }
        public bool RoomActivityStatus(Room room)
        {
            return room.RoomRenovationState.ActivityStatus;
        }
    }
}