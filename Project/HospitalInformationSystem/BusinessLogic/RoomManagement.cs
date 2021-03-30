/***********************************************************************
 * Module:  RoomManagement.cs
 * Author:  Mirko
 * Purpose: Definition of the Class BusinessLogic.RoomManagement
 ***********************************************************************/

using Model;
using HospitalInformationSystem.Windows;

namespace BusinessLogic
{
    public class RoomManagement
    {
        public RoomManagement()
        {
            // TODO: implement
        }
        public void createRoom(int floor, int id, string name, TypeOfRoom type)
        {
            // TODO: implement
            Room newRoom = new Room(id, name, floor, type);

            RoomDataBase.getInstance().addRoom(newRoom);

        }

        public void deleteRoom(Room room)
        {
            // TODO: implement
            RoomDataBase.getInstance().removeRoom(room);
        }

        public void deleteAllRooms()
        {
            // TODO: implement
            RoomDataBase.getInstance().removeAllRoom();
            
        }

        public void changeRoom(Room room, int newId, string newName, TypeOfRoom newType, int newFloor)
        {
            // TODO: implement
            room.Id = newId;
            room.Name = newName;
            room.Type = newType;
            room.Floor = newFloor;

        }
    }
}