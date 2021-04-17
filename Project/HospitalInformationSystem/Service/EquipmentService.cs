using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Service
{
    class EquipmentService
    {

        List<Equipment> equipmentList;
        public EquipmentService()
        {
            equipmentList = new List<Equipment>();
        }

        public void addNewEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;
            if (equipmentList == null)
                equipmentList = new List<Equipment>();
            if (!equipmentList.Contains(equipment))
                equipmentList.Add(equipment);
        }
    }
}
