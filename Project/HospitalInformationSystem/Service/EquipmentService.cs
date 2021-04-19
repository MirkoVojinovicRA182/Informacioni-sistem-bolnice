using HospitalInformationSystem.Repository;
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
        EquipmentRepository er;
        public EquipmentService()
        {
            equipmentList = new List<Equipment>();
            er = new EquipmentRepository();
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

        public List<Equipment> getStaticEquipment()
        {
            List<Equipment> staticEquipment = new List<Equipment>();

            foreach(Equipment equipment in this.equipmentList)
            {
                if (equipment.Type == TypeOfEquipment.Static)
                    staticEquipment.Add(equipment);
            }

            return staticEquipment;
        }

        public List<Equipment> getDynamicEquipment()
        {
            List<Equipment> dynamicEquipment = new List<Equipment>();

            foreach (Equipment equipment in this.equipmentList)
            {
                if (equipment.Type == TypeOfEquipment.Dynamic)
                    dynamicEquipment.Add(equipment);
            }

            return dynamicEquipment;
        }

        public List<Equipment> getEquipment()
        {
            return this.equipmentList;
        }

        public void deleteEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;
            if (this.equipmentList != null)
                if (this.equipmentList.Contains(equipment))
                    this.equipmentList.Remove(equipment);
        }

        public void changeEquipment(Equipment equipment, string id, string name, TypeOfEquipment typeOfEquipment, int quantityInMagacine, string description)
        {
            equipment.Id = id;
            equipment.Name = name;
            equipment.Type = typeOfEquipment;
            equipment.QuantityInMagacine = quantityInMagacine;
            equipment.Description = description;
        }

        public void saveInFile()
        {
            this.er.saveInFile(this.equipmentList);
        }

        public void loadFromFile()
        {
            List<Equipment> list = this.er.loadFromFile();
            removeAllEquipment();
            foreach (Equipment eq in list)
                addNewEquipment(eq);
        }


        public void removeAllEquipment()
        {
            if (this.equipmentList != null)
                this.equipmentList.Clear();
        }

        public void changeQuantityInMagacine(string id, int quantity)
        {
            foreach(Equipment equipment in equipmentList)
            {
                if (string.Equals(equipment.Id, id))
                    equipment.QuantityInMagacine -= quantity;
            }
        }

        public void moveEquipmentInMagacine(string id, int quantity)
        {
            foreach (Equipment equipment in equipmentList)
            {
                if (string.Equals(equipment.Id, id))
                    equipment.QuantityInMagacine += quantity;
            }
        }
    }
}
