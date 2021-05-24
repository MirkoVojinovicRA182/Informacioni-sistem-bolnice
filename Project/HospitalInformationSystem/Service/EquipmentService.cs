using HospitalInformationSystem.Repository;
using HospitalInformationSystem.Utility;
using Model;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace HospitalInformationSystem.Service
{
    class EquipmentService
    {
        private EquipmentRepository _equipmentRepository;
        private ObservableCollection<Equipment> _roomEquipment;
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
        public ObservableCollection<Equipment> MakeEquipmentForRoom(string equipmentType, Hashtable currentRoomEquipment)
        {
            LoadAllEquipment(equipmentType);
            LoadJustMissingEquipment(currentRoomEquipment);
            return _roomEquipment;
        }
        private void LoadAllEquipment(string equipmentType)
        {
            if (equipmentType.Equals(Constants.DYNAMIC_EQUIPMENT))
                _roomEquipment = new ObservableCollection<Equipment>(getDynamicEquipment());
            else
                _roomEquipment = new ObservableCollection<Equipment>(getStaticEquipment());
        }
        private void LoadJustMissingEquipment(Hashtable currentRoomEquipment)
        {
            foreach (DictionaryEntry de in currentRoomEquipment)
            {
                _roomEquipment.Remove(findEquipmentById(de.Key.ToString()));
            }
        }
    }
}
