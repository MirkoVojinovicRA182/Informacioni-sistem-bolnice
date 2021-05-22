using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.DTO
{
    public class EquipmentDTO
    {
        public EquipmentDTO(string name, TypeOfEquipment type, string description, int newQuantity, int currentQuantityInMagacine)
        {
            Name = name;
            Type = type;
            Description = description;
            NewQuantity = newQuantity;
            CurrentQuantityInMagacine = currentQuantityInMagacine;
        }

        public string Name { get; set; }
        public TypeOfEquipment Type { get; set; }
        public string Description { get; set; }
        public int NewQuantity { get; set; }
        public int CurrentQuantityInMagacine { get; set; }
    }
}
