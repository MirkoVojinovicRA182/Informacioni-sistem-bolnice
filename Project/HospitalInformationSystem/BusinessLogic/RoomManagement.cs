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
        public int CreateRoom(int floor, int id, string name)
        {
            // TODO: implement
            Room newRoom = new Room(id, name, floor);

            RoomDataBase.getInstance().AddRoom(newRoom);

            return RoomDataBase.getInstance().GetRoom().Count;

        }

        public bool DeleteRoom(Room room)
        {
            // TODO: implement
            return false;
        }

        public bool DeleteAllRooms()
        {
            // TODO: implement
            return false;
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

        public RoomManagement()
        {
            // TODO: implement
        }

    }
}