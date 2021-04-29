using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    class Medicine
    {
        public Medicine(int id, string name, TypeOfMedicine type, string purpose, string wayOfUse, Medicine replacementMedicine)
        {
            Id = id;
            Name = name;
            Type = type;
            Purpose = purpose;
            WayOfUse = wayOfUse;
            ReplacementMedicine = replacementMedicine;
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
                if (Type == TypeOfMedicine.Rastvor)
                    return "Rastvor";
                else if (Type == TypeOfMedicine.Sirup)
                    return "Sirup";
                else
                    return "Tableta";
            }
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

        public List<MedicineIngredient> Ingredients
        {
            get; set;
        }
    }
}
