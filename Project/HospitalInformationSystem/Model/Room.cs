using HospitalInformationSystem.DTO;
using HospitalInformationSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Room
    {
        private string stringValueOfEnumType;
        private RoomEquipment _equipmentInRoom;
        public Room(int id, string name, int floor, TypeOfRoom type, Hashtable equipment)
        {
            this.Id = id;
            this.Name = name;
            this.Floor = floor;
            this.Type = type;
            _equipmentInRoom = new RoomEquipment(equipment);
            RoomRenovationState = new RoomRenovationState(DateTime.Now, DateTime.Now);
        }
        public string Name
        {
            get; set;
        }
        public int Floor
        {
            get; set;
        }
        public int Id
        {
            get; set;
        }
        public TypeOfRoom Type
        {
            get; set;
        }

        public RoomEquipment EquipmentInRoom
        {
            get { return _equipmentInRoom; }
            set { _equipmentInRoom = value; }
        }

        public string StringValueOfEnumType
        {
            get
            {
                if (this.Type == TypeOfRoom.ExaminationRoom)
                    stringValueOfEnumType = "Prostorija za preglede";
                else if (this.Type == TypeOfRoom.HospitalizationRoom)
                    stringValueOfEnumType = "Sala za hospitalizaciju";
                else if (this.Type == TypeOfRoom.Office)
                    stringValueOfEnumType = "Kancelarija";
                else if (this.Type == TypeOfRoom.OperationRoom)
                    stringValueOfEnumType = "Operaciona sala";
                else if (this.Type == TypeOfRoom.RestRoom)
                    stringValueOfEnumType = "Prostorija za odmor";
                else if (this.Type == TypeOfRoom.RoomWithBeds)
                    stringValueOfEnumType = "Soba sa krevetima";

                return stringValueOfEnumType;
            }
            set { }
        }
        public RoomRenovationState RoomRenovationState
        {
            get; set;
        }
        public void UpdateProperties(RoomDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Type = dto.Type;
            Floor = dto.Floor;
        }
    }
}