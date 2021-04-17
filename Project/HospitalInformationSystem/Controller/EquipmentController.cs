using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Controller
{
    class EquipmentController
    {
        EquipmentService equipmentService;
        public EquipmentController()
        {
            equipmentService = new EquipmentService();
        }

        public void addNewEquipment(Equipment equipment)
        {
            equipmentService.addNewEquipment(equipment);
        }
    }
}
