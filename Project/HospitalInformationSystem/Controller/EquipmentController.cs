using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Controller
{
    class EquipmentController
    {
        EquipmentService _equipmentService;
        private static EquipmentController instance = null;

        public static EquipmentController getInstance()
        {
            if (instance == null)
                instance = new EquipmentController();
            return instance;
        }
        private EquipmentController()
        {
            _equipmentService = new EquipmentService();
        }

        public void addNewEquipment(Equipment equipment)
        {
            _equipmentService.addNewEquipment(equipment);
        }

        public List<Equipment> getStaticEquipment()
        {
            return _equipmentService.getStaticEquipment();
        }

        public List<Equipment> getDynamicEquipment()
        {
            return _equipmentService.getDynamicEquipment();
        }

        public List<Equipment> getEquipment()
        {
            return this._equipmentService.getEquipment();
        }
        public void deleteEquipment(Equipment equipment)
        {
            _equipmentService.deleteEquipment(equipment);
            RoomController.GetInstance().RemoveEquipmentFromRooms(equipment);
        }
        public void saveInFile()
        {
            _equipmentService.saveInFile();
        }

        public void loadFromFile()
        {
            _equipmentService.loadFromFile();
        }
        public string getEquipmentName(string id)
        {
            return _equipmentService.getEquipmentNameById(id);
        }

        public string getEquipmentId(string name)
        {
            return _equipmentService.getEquipmentIdByName(name);
        }

        public TypeOfEquipment getEquipmentType(string id)
        {
            return _equipmentService.getEquipmentTypeById(id);
        }

        public Equipment findEquipmentById(string id)
        {
            return _equipmentService.findEquipmentById(id);
        }
    }
}
