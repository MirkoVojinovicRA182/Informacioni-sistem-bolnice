using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    class Medicine
    {
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
