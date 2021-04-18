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

        private static EquipmentController instance = null;

        public static EquipmentController getInstance()
        {
            if (instance == null)
                instance = new EquipmentController();
            return instance;
        }
        private EquipmentController()
        {
            equipmentService = new EquipmentService();
        }

        public void addNewEquipment(Equipment equipment)
        {
            equipmentService.addNewEquipment(equipment);
        }

        public List<Equipment> getStaticEquipment()
        {
            return equipmentService.getStaticEquipment();
        }

        public List<Equipment> getDynamicEquipment()
        {
            return equipmentService.getDynamicEquipment();
        }

        public List<Equipment> getEquipment()
        {
            return this.equipmentService.getEquipment();
        }

        public void deleteEquipment(Equipment equipment)
        {
            equipmentService.deleteEquipment(equipment);
        }

        public void changeEquipment(Equipment equipment, string id, string name, TypeOfEquipment typeOfEquipment, int quantity, string description)
        {
            equipmentService.changeEquipment(equipment, id, name, typeOfEquipment, quantity, description);
        }
    }
}
