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

        ///public System.Collections.ArrayList equipment;

        /// <pdGenerated>default getter</pdGenerated>
        /*public System.Collections.ArrayList GetEquipment()
        {
            if (equipment == null)
                equipment = new System.Collections.ArrayList();
            return equipment;
        }*/

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

        /// <pdGenerated>default setter</pdGenerated>
       /* public void SetEquipment(System.Collections.ArrayList newEquipment)
        {
            RemoveAllEquipment();
            foreach (Equipment oEquipment in newEquipment)
                AddEquipment(oEquipment);
        }*/

        public void addEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipmentList == null)
                this.equipmentList = new List<Equipment>();
            if (!this.equipmentList.Contains(newEquipment))
                this.equipmentList.Add(newEquipment);
        }

        /// <pdGenerated>default Add</pdGenerated>
        /*public void AddEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new System.Collections.ArrayList();
            if (!this.equipment.Contains(newEquipment))
                this.equipment.Add(newEquipment);
        }*/

        public void removeEquipment(Equipment oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (this.equipmentList != null)
                if (this.equipmentList.Contains(oldEquipment))
                    this.equipmentList.Remove(oldEquipment);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        /*public void RemoveEquipment(Equipment oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (this.equipment != null)
                if (this.equipment.Contains(oldEquipment))
                    this.equipment.Remove(oldEquipment);
        }*/

        public void removeAllEquipment()
        {
            if (equipmentList != null)
                equipmentList.Clear();
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        /*public void RemoveAllEquipment()
        {
            if (equipment != null)
                equipment.Clear();
        }
        */

    }
}