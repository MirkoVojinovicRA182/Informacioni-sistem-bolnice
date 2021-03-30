/***********************************************************************
 * Module:  Equipment.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Equipment
 ***********************************************************************/

namespace Model
{
    public class Equipment
    {
        public string Id
        {
            get
            {
                // TODO: implement
                return (string)"";
            }
            set
            {
                // TODO: implement
            }
        }

        public TypeOfEquipment Type
        {
            get
            {
                // TODO: implement
                return TypeOfEquipment.Clothing;
            }
            set
            {
                // TODO: implement
            }
        }

        public int Quantity
        {
            get; set;
        }

        public string Name
        {
            get; set;
         
        }

    }
}