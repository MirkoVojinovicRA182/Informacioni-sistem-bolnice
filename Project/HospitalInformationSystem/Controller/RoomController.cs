using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Controller
{
    class RoomController
    {
        RoomService roomService;
        
        private static RoomController instance = null;

        public static RoomController getInstance()
        {
            if (instance == null)
                instance = new RoomController();
            return instance;
        }
        private RoomController()
        {
            roomService = new RoomService();
        }

        public void saveInFile()
        {
            roomService.saveInFile();
        }

        public void loadFromFile()
        {
            roomService.loadFromFile();
        }

        public void createRoom(int floor, int id, string name, TypeOfRoom type, Hashtable equipment)
        {
            roomService.createRoom(floor, id, name, type, equipment);
        }

        public void changeRoom(Room room, int newId, string newName, TypeOfRoom newType, int newFloor)
        {
            roomService.changeRoom(room, newId, newName, newType, newFloor);
        }

        public void setRoomEquipment(Room room, Hashtable eq)
        {
            roomService.setRoomEquipment(room, eq);
        }

        public void deleteEquipment(string id)
        {
            roomService.deleteEquipment(id);
        }

        public void deleteRoom(Room room)
        {
            roomService.deleteRoom(room);
        }

        public List<Room> getRooms()
        {
            return roomService.getRooms();
        }

        public void setRooms(List<Room> rooms)
        {
            roomService.setRooms(rooms);
        }

        public void changeStaticEquipmentState(Room room, int currentQuantity, int moveQuantity, string key)
        {
            roomService.changeStaticEquipmentState(room, currentQuantity, moveQuantity, key);
        }

        public void moveStaticEqToNextRoom(Room room, int moveQuantity, string key)
        {
            roomService.moveStaticEqToNextRoom(room, moveQuantity, key);
        }

        public bool checkId(int id)
        {
            return roomService.checkId(id);
        }
    }
}
