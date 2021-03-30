/***********************************************************************
 * Module:  RoomDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.RoomDataBase
 ***********************************************************************/

using System.Collections.Generic;

namespace Model
{
    public class RoomDataBase
    {

        private static RoomDataBase instance;

        public List<Room> rooms;

        public static RoomDataBase getInstance()
        {
            if (instance == null)
                instance = new RoomDataBase();
            return instance;
        }
        private RoomDataBase()
        {
            // TODO: implement
        }

        ~RoomDataBase()
        {
            // TODO: implement
        }

        //public System.Collections.ArrayList room;

        public List<Room> getRooms()
        {
            if (rooms == null)
                rooms = new List<Room>();
            return rooms;
        }
        public void setRoom(List<Room> newRoom)
        {
            removeAllRoom();
            foreach (Room oRoom in newRoom)
                addRoom(oRoom);
        }


        public void addRoom(Room newRoom)
        {
            if (newRoom == null)
                return;
            if (this.rooms == null)
                this.rooms = new List<Room>();
            if (!this.rooms.Contains(newRoom))
                this.rooms.Add(newRoom);
        }

       

        public void removeRoom(Room oldRoom)
        {
            if (oldRoom == null)
                return;
            if (this.rooms != null)
                if (this.rooms.Contains(oldRoom))
                    this.rooms.Remove(oldRoom);
        }

        public void removeAllRoom()
        {
            if (rooms != null)
                rooms.Clear();
        }

    }
}