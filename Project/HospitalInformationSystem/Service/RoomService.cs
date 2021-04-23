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

namespace Service
{
    public class RoomService
    {
        public RoomService()
        {
            // TODO: implement
        }
        public void createRoom(int floor, int id, string name, TypeOfRoom type, Hashtable equipment)
        {
            // TODO: implement
            Room newRoom = new Room(id, name, floor, type);
            newRoom.Equipment = equipment;

            RoomDataBase.getInstance().addRoom(newRoom);

        }

        public void deleteRoom(Room room)
        {
            // TODO: implement
            RoomDataBase.getInstance().removeRoom(room);


            //brisanje termina koji se odvijaju u datoj prostoriji
            AppointmentManagement appointmentManagement = new AppointmentManagement();
            List<Appointment> list = appointmentManagement.findAppointmentByRoom(room);

            foreach(Appointment appointment in list)
            {
                appointmentManagement.deleteAppointment(appointment);
            }
        }

        public void deleteAllRooms()
        {
            // TODO: implement
            RoomDataBase.getInstance().removeAllRoom();
            
        }

        public void changeRoom(Room room, int newId, string newName, TypeOfRoom newType, int newFloor, Hashtable equipment)
        {
            // TODO: implement
            room.Id = newId;
            room.Name = newName;
            room.Type = newType;
            room.Floor = newFloor;
            room.Equipment = equipment;

        }

        public void deleteEquipment(string id)
        {
            List<Room> rooms = RoomDataBase.getInstance().getRooms();
            foreach(Room room in rooms)
            {
                if (room.Equipment.Contains(id))
                    room.Equipment.Remove(id);
            }
        }

        public List<Room> getRooms()
        {
            return RoomDataBase.getInstance().getRooms();
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
    }
}