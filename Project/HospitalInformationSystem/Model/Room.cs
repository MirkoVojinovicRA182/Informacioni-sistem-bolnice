/***********************************************************************
 * Module:  Room.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

namespace Model
{
    public class Room
    {
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

        public System.Collections.ArrayList equipment;

        /// <pdGenerated>default getter</pdGenerated>
        public System.Collections.ArrayList GetEquipment()
        {
            if (equipment == null)
                equipment = new System.Collections.ArrayList();
            return equipment;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetEquipment(System.Collections.ArrayList newEquipment)
        {
            RemoveAllEquipment();
            foreach (Equipment oEquipment in newEquipment)
                AddEquipment(oEquipment);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new System.Collections.ArrayList();
            if (!this.equipment.Contains(newEquipment))
                this.equipment.Add(newEquipment);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveEquipment(Equipment oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (this.equipment != null)
                if (this.equipment.Contains(oldEquipment))
                    this.equipment.Remove(oldEquipment);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllEquipment()
        {
            if (equipment != null)
                equipment.Clear();
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

    }
}