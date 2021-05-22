using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    [Serializable]
    public class Medicine
    {
        public Medicine(int id, string name, TypeOfMedicine type, string purpose, string wayOfUse, Medicine replacementMedicine, List<MedicineIngredient> medicineIngredients)
        {
            Id = id;
            Name = name;
            Type = type;
            Purpose = purpose;
            WayOfUse = wayOfUse;
            ReplacementMedicine = replacementMedicine;
            Ingredients = medicineIngredients;
        }
        public Medicine(int id, string name, TypeOfMedicine type, string purpose, string wayOfUse, List<MedicineIngredient> medicineIngredients)
        {
            Id = id;
            Name = name;
            Type = type;
            Purpose = purpose;
            WayOfUse = wayOfUse;
            Ingredients = medicineIngredients;
        }
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public TypeOfMedicine Type
        {
            get; set;
        }
        public string StringValueOfType
        {
            get
            {
                if (Type == TypeOfMedicine.Dilution)
                    return "Rastvor";
                else if (Type == TypeOfMedicine.Syrup)
                    return "Sirup";
                else if (Type == TypeOfMedicine.Tablet)
                    return "Tableta";
                else
                    return "Pilula";
            }
            set { }
        }
        public string Purpose
        {
            get; set;
        }
        public string WayOfUse
        {
            get; set;
        }
        public Medicine ReplacementMedicine
        {
            get; set;
        }

        public string NameOfReplacementMedicine
        {
            get
            {
                if (ReplacementMedicine != null)
                    return ReplacementMedicine.Name;
                return "nema";
            }
            set { }
        }

        public List<MedicineIngredient> Ingredients
        {
            get; set;
        }

        public string Comment
        {
            get; set;
        }

        public void UpdateMedicine(Medicine newMedicine)
        {
            Id = newMedicine.Id;
            Name = newMedicine.Name;
            Type = newMedicine.Type;
            Purpose = newMedicine.Purpose;
            WayOfUse = newMedicine.WayOfUse;
            ReplacementMedicine = newMedicine.ReplacementMedicine;
            Ingredients = newMedicine.Ingredients;
        }
    }
}
