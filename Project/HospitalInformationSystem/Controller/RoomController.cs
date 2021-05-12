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
        public void SaveRoomsInFile()
        {
            _roomService.SaveRoomsInFile();
        }
        public void LoadRoomsFromFile()
        {
            _roomService.LoadRoomsFromFile();
        }
        public void CreateRoom(Room newRoom)
        {
            _roomService.CreateRoom(newRoom);
        }
        public void DeleteRoom(Room room)
        {
            _roomService.DeleteRoom(room);
            AppointmentController.getInstance().DeleteAllAppointmentsFromRoom(room);
        }
        public void ChangeRoom(Room roomForChange, Room roomDTO)
        {
            _roomService.ChangeRoom(roomForChange, roomDTO);
        }
        public void SetRoomEquipment(Room room, Hashtable eq)
        {
            _roomService.SetRoomEquipment(room, eq);
        }
        public List<Room> GetRooms()
        {
            return _roomService.GetRooms();
        }
        public void SetRooms(List<Room> rooms)
        {
            _roomService.SetRooms(rooms);
        }
        public void ChangeStaticEquipmentState(Room room, int currentQuantity, int moveQuantity, string key)
        {
            _roomService.ChangeStaticEquipmentState(room, currentQuantity, moveQuantity, key);
        }
        public void MoveStaticEqToNextRoom(Room room, int moveQuantity, string key)
        {
            _roomService.MoveStaticEqToNextRoom(room, moveQuantity, key);
        }
        public bool RoomExists(int roomId)
        {
            return _roomService.RoomExists(roomId);
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
        public bool RoomActivityStatus(Room room)
        {
            return _roomService.RoomActivityStatus(room);
        }
    }
}
