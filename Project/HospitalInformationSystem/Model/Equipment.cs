/***********************************************************************
 * Module:  Equipment.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Equipment
 ***********************************************************************/

using System;

namespace Model
{
    [Serializable]
    public class Equipment
    {

        public Equipment(string id, string name, TypeOfEquipment typeOfEquipment, int quantity, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Type = typeOfEquipment;
            this.Quantity = quantity;
            this.Description = description;
        }
        public string Id
        {
            get; set;
        }

        public string Name
        {
            get; set;

        }

        public TypeOfEquipment Type
        {
            get; set;
        }

        public int Quantity
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

    }
}