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
        private EquipmentService _equipmentService;
        private AppointmentService _appointmentService;
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
            _appointmentService = new AppointmentService();
            _equipmentService = new EquipmentService();
        }
        public void SaveRoomsInFile()
        {
            _roomService.SaveRoomsInFile();
        }
        public void LoadRoomsFromFile()
        {
            _roomService.LoadRoomsFromFile();
        }
        public void AddNewRoom(Room newRoom)
        {
            _roomService.AddNewRoom(newRoom);
        }
        public void DeleteRoom(Room room)
        {
            _roomService.DeleteRoom(room);
            _appointmentService.DeleteAllAppointmentsFromRoom(room);
            _equipmentService.MoveEquipmentFromRoomToMagacine(room);
        }
        public void SetRoomEquipment(Room room, Hashtable eq)
        {
            _roomService.SetRoomEquipment(room, eq);
        }
        public List<Room> GetRooms()
        {
            return _roomService.GetRooms();
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
        public void ChangeRoomActivityStatus(Room roomForRenovation)
        {
            _roomService.ChangeRoomActivityStatus(roomForRenovation);
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
