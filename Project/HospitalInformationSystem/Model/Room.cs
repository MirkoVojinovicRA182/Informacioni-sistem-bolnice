/***********************************************************************
 * Module:  Room.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

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

        public Room(int id, string name, int floor, TypeOfRoom type)
        {
            this.Id = id;
            this.Name = name;
            this.Floor = floor;
            this.Type = type;
            Equipment = new Hashtable();
            RoomRenovationState = new RoomRenovationState(DateTime.Now, DateTime.Now);
        }
        public Room(int id, string name, int floor, TypeOfRoom type, Hashtable equipment)
        {
            this.Id = id;
            this.Name = name;
            this.Floor = floor;
            this.Type = type;
            Equipment = equipment;
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
            get;
            set;
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
        public void EditProperties(Room roomDTO)
        {
            this.Id = roomDTO.Id;
            this.Name = roomDTO.Name;
            this.Type = roomDTO.Type;
            this.Floor = roomDTO.Floor;
        }
    }
}