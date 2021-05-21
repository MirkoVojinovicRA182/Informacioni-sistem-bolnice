/***********************************************************************
 * Module:  Room.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System.Collections;

namespace Model
{
    public class RoomEquipment
    {
        private Hashtable _equipment;
        public Hashtable Equipment
        {
            get { return _equipment; }
            set
            {
                foreach (DictionaryEntry de in value)
                    _equipment.Add(de.Key, de.Value);
            }
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