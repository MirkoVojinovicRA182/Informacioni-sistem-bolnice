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
        public void SaveInFile()
        {
            _roomService.saveInFile();
        }
        public void LoadFromFile()
        {
            _roomService.loadFromFile();
        }
        public void CreateRoom(int floor, int id, string name, TypeOfRoom type, Hashtable equipment)
        {
            _roomService.createRoom(floor, id, name, type, equipment);
        }
        public void ChangeRoom(Room room, int newId, string newName, TypeOfRoom newType, int newFloor)
        {
            _roomService.changeRoom(room, newId, newName, newType, newFloor);
        }
        public void SetRoomEquipment(Room room, Hashtable eq)
        {
            _roomService.setRoomEquipment(room, eq);
        }
        public void DeleteEquipment(string id)
        {
            _roomService.deleteEquipment(id);
        }
        public void DeleteRoom(Room room)
        {
            _roomService.deleteRoom(room);
        }
        public List<Room> GetRooms()
        {
            return _roomService.getRooms();
        }
        public void SetRooms(List<Room> rooms)
        {
            _roomService.setRooms(rooms);
        }
        public void ChangeStaticEquipmentState(Room room, int currentQuantity, int moveQuantity, string key)
        {
            _roomService.changeStaticEquipmentState(room, currentQuantity, moveQuantity, key);
        }
        public void MoveStaticEqToNextRoom(Room room, int moveQuantity, string key)
        {
            _roomService.moveStaticEqToNextRoom(room, moveQuantity, key);
        }
        public bool FindRoom(int id)
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
