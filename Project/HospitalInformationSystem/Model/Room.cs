/***********************************************************************
 * Module:  Room.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Room
    {
        private string typeOfRoom;

        public List<Equipment> equipmentList;
        public Room()
        {
            // TODO: implement
        }

        public Room(int id, string name, int floor, TypeOfRoom type)
        {
            this.Id = id;
            this.Name = name;
            this.Floor = floor;
            this.Type = type;
        }

        ~Room()
        {
            // TODO: implement
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

        public string StringValuesOfEnumType
        {
            get
            {
                if (this.Type == TypeOfRoom.ExaminationRoom)
                    typeOfRoom = "Prostorija za preglede";
                else if (this.Type == TypeOfRoom.HospitalizationRoom)
                    typeOfRoom = "Sala za hospitalizaciju";
                else if (this.Type == TypeOfRoom.Office)
                    typeOfRoom = "Kancelarija";
                else if (this.Type == TypeOfRoom.OperationRoom)
                    typeOfRoom = "Operaciona sala";
                else if (this.Type == TypeOfRoom.RestRoom)
                    typeOfRoom = "Prostorija za odmor";
                else if (this.Type == TypeOfRoom.RoomWithBeds)
                    typeOfRoom = "Soba sa krevetima";

                return typeOfRoom;
            }
            set { }
        }
        public List<Equipment> getEquipmentList()
        {
            if (equipmentList == null)
                equipmentList = new List<Equipment>();
            return equipmentList;
        }

        public void setEquipmentList(List<Equipment> newEquipment)
        {
            removeAllEquipment();
            foreach (Equipment oEquipment in newEquipment)
                addEquipment(oEquipment);
        }

       

        public void addEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipmentList == null)
                this.equipmentList = new List<Equipment>();
            if (!this.equipmentList.Contains(newEquipment))
                this.equipmentList.Add(newEquipment);
        }

       

        public void removeEquipment(Equipment oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (this.equipmentList != null)
                if (this.equipmentList.Contains(oldEquipment))
                    this.equipmentList.Remove(oldEquipment);
        }

      

        public void removeAllEquipment()
        {
            if (equipmentList != null)
                equipmentList.Clear();
        }


    }
}