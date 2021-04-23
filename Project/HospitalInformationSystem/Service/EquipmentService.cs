﻿using HospitalInformationSystem.Repository;
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

        public void changeEquipment(Equipment equipment, string id, string name, TypeOfEquipment typeOfEquipment, int newQuantity, int oldQuantity, string description)
        {
            equipment.Id = id;
            equipment.Name = name;
            equipment.Type = typeOfEquipment;
            equipment.QuantityInMagacine = newQuantity;
            equipment.Description = description;
            if (newQuantity > oldQuantity)
                equipment.Quantity += newQuantity - oldQuantity;
            else
                equipment.Quantity -= oldQuantity - newQuantity;
        }

        public void saveInFile()
        {
            this.er.saveInFile(this.equipmentList);
        }

        public void loadFromFile()
        {
            equipmentList = this.er.loadFromFile();
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

        public void removeQuantity(string id, int quantity)
        {
            foreach (Equipment equipment in equipmentList)
            {
                if (string.Equals(equipment.Id, id))
                    equipment.Quantity -= quantity;
            }
        }

        public string getEquipmentName(string id)
        {
            String foundedName = "";
            foreach(Equipment eq in equipmentList)
            {
                if (string.Equals(eq.Id, id))
                    foundedName = eq.Name;
            }
            return foundedName;
        }

        public string getEquipmentId(string name)
        {
            String foundedId = "";
            foreach (Equipment eq in equipmentList)
            {
                if (string.Equals(eq.Name, name))
                    foundedId = eq.Id;
            }
            return foundedId;
        }

        public TypeOfEquipment getEquipmentType(string id)
        {
            TypeOfEquipment type = TypeOfEquipment.Static;
            foreach (Equipment eq in equipmentList)
            {
                if (string.Equals(eq.Id, id))
                    type = eq.Type;
            }
            return type;
        }

        public Equipment findEquipment(string id)
        {
            foreach(Equipment eq in equipmentList)
            {
                if (string.Equals(eq.Id, id))
                    return eq;
            }
            return null;
        }
    }
}