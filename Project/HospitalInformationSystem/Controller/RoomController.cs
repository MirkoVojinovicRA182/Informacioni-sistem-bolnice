using HospitalInformationSystem.Model;
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
        private RoomService _roomService;
        private static RoomController _instance;
        public static RoomController GetInstance()
        {
            if (_instance == null)
                _instance = new RoomController();
            return _instance;
        }
        private RoomController()
        {
            _roomService = new RoomService();
        }
        public void saveInFile()
        {
            _roomService.saveInFile();
        }
        public void loadFromFile()
        {
            _roomService.loadFromFile();
        }
        public void createRoom(int floor, int id, string name, TypeOfRoom type, Hashtable equipment)
        {
            _roomService.createRoom(floor, id, name, type, equipment);
        }
        public void changeRoom(Room room, int newId, string newName, TypeOfRoom newType, int newFloor)
        {
            _roomService.changeRoom(room, newId, newName, newType, newFloor);
        }
        public void setRoomEquipment(Room room, Hashtable eq)
        {
            _roomService.setRoomEquipment(room, eq);
        }
        public void deleteEquipment(string id)
        {
            _roomService.deleteEquipment(id);
        }
        public void deleteRoom(Room room)
        {
            _roomService.deleteRoom(room);
        }
        public List<Room> getRooms()
        {
            return _roomService.getRooms();
        }
        public void setRooms(List<Room> rooms)
        {
            _roomService.setRooms(rooms);
        }
        public void changeStaticEquipmentState(Room room, int currentQuantity, int moveQuantity, string key)
        {
            _roomService.changeStaticEquipmentState(room, currentQuantity, moveQuantity, key);
        }
        public void moveStaticEqToNextRoom(Room room, int moveQuantity, string key)
        {
            _roomService.moveStaticEqToNextRoom(room, moveQuantity, key);
        }
        public bool findRoom(int id)
        {
            return _roomService.findRoom(id);
        }
        public void SetRenovationStateToRoom(Room room, RoomRenovationState roomRenovationState)
        {
            _roomService.SetRenovationStateToRoom(room, roomRenovationState);
        }
        public void CheckRenovationTerm(Room roomForRenovation)
        {
            _roomService.CheckRenovationTerm(roomForRenovation);
        }
        public Room GetMagacine()
        {
            return _roomService.GetMagacine();
        }
        public void AddEquipmentToMagacine(Equipment newEquipment)
        {
            _roomService.AddEquipmentToMagacine(newEquipment);
        }
        public void AddRoomToRoomList(Room newRoom)
        {
            _roomService.AddRoomToRoomList(newRoom);
        }
        public List<Appointment> GetAppointmentsInRoom(string nameOfRoom)
        {
            return _roomService.GetAppointmentsInRoom(nameOfRoom);
        }
    }
}
