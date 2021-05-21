/***********************************************************************
 * Module:  Room.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

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
        private Hashtable _equipment;
        public Room(int id, string name, int floor, TypeOfRoom type, Hashtable equipment)
        {
            this.Id = id;
            this.Name = name;
            this.Floor = floor;
            this.Type = type;
            _equipment = equipment;
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

        public Hashtable Equipment
        {
            get { return _equipment; }
            set
            {
                foreach (DictionaryEntry de in value)
                    _equipment.Add(de.Key, de.Value);
            }
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
            this.Id = dto.Id;
            this.Name = dto.Name;
            this.Type = dto.Type;
            this.Floor = dto.Floor;
        }
        public void ChangeEquipmentState(int quantityForMove, string key)
        {
            if (((int)_equipment[key] - quantityForMove) == 0)
            {
                _equipment.Remove(key);
                return;
            }
            _equipment[key] = (int)_equipment[key] - quantityForMove;
        }
        public void AcceptEquipmentFromOtherRoom(int moveQuantity, string key)
        {
            if (_equipment.Contains(key))
                _equipment[key] = (int)_equipment[key] + moveQuantity;
            else
                _equipment.Add(key, moveQuantity);
        }
    }
}