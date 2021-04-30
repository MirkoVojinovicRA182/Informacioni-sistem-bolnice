using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    [Serializable]
    public class MedicineIngredient
    {
        public MedicineIngredient(string name, double quantity, int rdi)
        {
            Name = name;
            QuantityInAHundredGrams = quantity;
            RecommendedDailyIntake = rdi;
        }
        public string Name
        {
            get; set;
        }
        public double QuantityInAHundredGrams
        {
            get; set;
        }
        public int RecommendedDailyIntake
        {
            get; set;
        }
    }
}
