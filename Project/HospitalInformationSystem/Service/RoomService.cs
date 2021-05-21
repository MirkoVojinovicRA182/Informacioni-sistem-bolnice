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
        public void DeleteRoom(Room room)
        {
            GetRooms().Remove(room);
        }
        public List<Room> GetRooms()
        {
            return _repository.GetRooms();
        }
        public bool RoomExists(int roomId)
        {
            return _repository.RoomExists(roomId);
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
    }
}