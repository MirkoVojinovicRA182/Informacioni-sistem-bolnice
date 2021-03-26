/***********************************************************************
 * Module:  RoomManagement.cs
 * Author:  Mirko
 * Purpose: Definition of the Class BusinessLogic.RoomManagement
 ***********************************************************************/

using System;
using Model;

namespace BusinessLogic
{
   public class RoomManagement
   {
      public bool CreateRoom(int floor, int id, string name)
      {
            // TODO: implement
            Room newRoom = new Room(id, name, floor);

            RoomDataBase roomDataBase = new RoomDataBase();

            roomDataBase.AddRoom(newRoom);

            return true;
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