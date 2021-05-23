using System;
using System.Collections;

namespace Model
{
    [Serializable]
    public class RoomEquipment
    {
        private Hashtable _equipment = new Hashtable();
        public RoomEquipment(Hashtable equipment)
        {
            _equipment = equipment;
        }
        public Hashtable Equipment { 
            get { return _equipment; }
            set { _equipment = value; }
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
        public void ReduceEquipmentQuantity(string equipmentId, int quantity)
        {
            _equipment[equipmentId] = (int)_equipment[equipmentId] - quantity;
        }
    }
}