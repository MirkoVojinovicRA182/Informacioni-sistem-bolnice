using HospitalInformationSystem.DTO;
using System;
namespace Model
{
    [Serializable]
    public class Equipment
    {
        public Equipment(string id, string name, TypeOfEquipment typeOfEquipment, int quantity, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Type = typeOfEquipment;
            this.Quantity = quantity;
            this.Description = description;
        }
        public string Id
        {
            get; set;
        }
        public string Name
        {
            get; set;

        }
        public TypeOfEquipment Type
        {
            get; set;
        }

        public int Quantity
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }
        public string GetStringType
        {
            get
            {
                if (this.Type == TypeOfEquipment.Static)
                    return "Statička";
                else
                    return "Dinamička";
            }
        }
        public void UpdateProperties(EquipmentDTO equipmentDTO)
        {
            Name = equipmentDTO.Name;
            Type = equipmentDTO.Type;
            Description = equipmentDTO.Description;
            if (equipmentDTO.NewQuantity > equipmentDTO.CurrentQuantityInMagacine)
                Quantity += equipmentDTO.NewQuantity - equipmentDTO.CurrentQuantityInMagacine;
            else
                Quantity -= equipmentDTO.CurrentQuantityInMagacine - equipmentDTO.NewQuantity;
        }
        public void ReduceQuantity(int quantity)
        {
            Quantity -= quantity;
        }
    }
}