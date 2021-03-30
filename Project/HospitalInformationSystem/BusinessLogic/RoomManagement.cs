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

            newRoom.getListEq().Add(new Equipment() { Id = "1a", Type = TypeOfEquipment.Clothing, Name = "Prva odeca", Quantity = 10 });
            newRoom.getListEq().Add(new Equipment() { Id = "1a", Type = TypeOfEquipment.Clothing, Name = "Druga odeca", Quantity = 10 });
            newRoom.getListEq().Add(new Equipment() { Id = "1a", Type = TypeOfEquipment.Clothing, Name = "Peta odeca", Quantity = 10 });

            RoomDataBase.getInstance().AddRoom(newRoom);


        }

        public bool DeleteRoom(Room room)
        {
            // TODO: implement
            RoomDataBase.getInstance().RemoveRoom(room);
            return false;
        }

        public void DeleteAllRooms()
        {
            // TODO: implement
            RoomDataBase.getInstance().RemoveAllRoom();
            
        }

        public void ChangeRoom(Room room, int newId, string newName, TypeOfRoom newType, int newFloor)
        {
            // TODO: implement
            room.Id = newId;
            room.Name = newName;
            room.Type = newType;
            room.Floor = newFloor;

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