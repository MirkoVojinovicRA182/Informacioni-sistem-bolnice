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
        public void CreateRoom(int floor, int id, string name, TypeOfRoom type)
        {
            // TODO: implement
            Room newRoom = new Room(id, name, floor, type);

            RoomDataBase.getInstance().AddRoom(newRoom);


        }

        public bool DeleteRoom(Room room)
        {
            // TODO: implement
            return false;
        }

        public void DeleteAllRooms()
        {
            // TODO: implement
            RoomDataBase.getInstance().RemoveAllRoom();
            
        }

        public bool ChangeRoom(Room room)
        {
            // TODO: implement
            return false;
        }

        public bool ReadRoom(Room room)
        {
            // TODO: implement
            return false;
        }

        public bool ReadAllRooms()
        {
            // TODO: implement
            return false;
        }

    }
}