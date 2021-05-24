using Model;
using System.Collections.Generic;
using System.Collections;
using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Repository;
namespace HospitalInformationSystem.Service
{
    public class RoomService
    {
        private RoomRepository _repository;
        public RoomService()
        {
            _repository = new RoomRepository();
        }
        public void SaveRoomsInFile()
        {
            _repository.saveInFile();
        }
        public void LoadRoomsFromFile()
        {
            _repository.loadFromFile();
        }
        public void AddRoomToRoomList(Room newRoom)
        {
            _repository.AddRoomToRoomList(newRoom);
        }
        public void DeleteRoom(Room room)
        {
            _repository.DeleteRoom(room);
        }
        public List<Room> GetRooms()
        {
            return _repository.GetRooms();
        }
        public bool RoomExists(int roomId)
        {
            return _repository.RoomExists(roomId);
        }
        public Room GetMagacine()
        {
            return _repository.GetMagacine();
        }
        public List<Appointment> GetAppointmentsInRoom(string nameOfRoom)
        {
            return _repository.GetAppointmentsInRoom(nameOfRoom);
        }
        public void RemoveEquipmentFromRooms(Equipment equipment)
        {
            foreach (Room room in RoomController.GetInstance().GetRooms())
            {
                if (room.EquipmentInRoom.Equipment.Contains(equipment.Id))
                    room.EquipmentInRoom.Equipment.Remove(equipment.Id);
            }
        }
        public void MoveRoomEquipmentToMagacine(Room room)
        {
            foreach (DictionaryEntry roomEquipment in room.EquipmentInRoom.Equipment)
            {
                GetMagacine().EquipmentInRoom.Equipment[(string)roomEquipment.Key] = (int)GetMagacine().EquipmentInRoom.Equipment[roomEquipment.Key] + (int)roomEquipment.Value;
            }
        }
        public bool EquipmentExistInRoom(string id, Hashtable roomEq)
        {
            return _repository.EquipmentExistInRoom(id, roomEq);
        }
    }
}