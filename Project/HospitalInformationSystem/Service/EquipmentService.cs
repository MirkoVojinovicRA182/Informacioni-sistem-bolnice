using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Repository;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Service
{
    class EquipmentService
    {
        EquipmentRepository _equipmentRepository;
        public EquipmentService()
        {
            _equipmentRepository = new EquipmentRepository();
        }
        public void addNewEquipment(Equipment equipment)
        {
            _equipmentRepository.addNewEquipment(equipment);
        }
        public List<Equipment> getStaticEquipment()
        {
            return _equipmentRepository.getStaticEquipment();
        }
        public List<Equipment> getDynamicEquipment()
        {
            return _equipmentRepository.getDynamicEquipment();
        }
        public List<Equipment> getEquipment()
        {
            return _equipmentRepository.getEquipment();
        }
        public void deleteEquipment(Equipment equipment)
        {
            _equipmentRepository.deleteEquipment(equipment);
        }
        public void saveInFile()
        {
            this._equipmentRepository.saveInFile();
        }
        public void loadFromFile()
        {
            _equipmentRepository.loadFromFile();
        }
        public string getEquipmentNameById(string id)
        {
            return _equipmentRepository.getEquipmentNameById(id);
        }
        public string getEquipmentIdByName(string name)
        {
            return _equipmentRepository.getEquipmentIdByName(name);
        }
        public TypeOfEquipment getEquipmentTypeById(string id)
        {
            return _equipmentRepository.getEquipmentTypeById(id);
        }
        public Equipment findEquipmentById(string id)
        {
            return _equipmentRepository.findEquipmentById(id);
        }
    }
}
