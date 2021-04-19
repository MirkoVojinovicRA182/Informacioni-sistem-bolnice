/***********************************************************************
 * Module:  RoomManagement.cs
 * Author:  Mirko
 * Purpose: Definition of the Class BusinessLogic.RoomManagement
 ***********************************************************************/

using Model;
using HospitalInformationSystem.Windows;
using HospitalInformationSystem.BusinessLogic;
using System.Collections.Generic;
using System.Collections;

namespace BusinessLogic
{
    public class RoomManagement
    {
        public RoomManagement()
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
            foreach(Room room in RoomDataBase.getInstance().getRooms())
            {
                foreach(DictionaryEntry de in room.Equipment)
                {
                    if (string.Equals(de.Key.ToString(), id))
                        room.Equipment.Remove(id);
                }
            }
        }
    }
}