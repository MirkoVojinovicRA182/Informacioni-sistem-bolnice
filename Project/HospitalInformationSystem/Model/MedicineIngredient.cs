using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public class MedicineIngredient
    {
        public MedicineIngredient(string name, int quantity, int rdi)
        {
            Name = name;
            QuantityInAHundredGrams = quantity;
            RecommendedDailyIntake = rdi;
        }
        public string Name
        {
            get; set;
        }
        public int QuantityInAHundredGrams
        {
            get; set;
        }
        public int RecommendedDailyIntake
        {
            get; set;
        }
    }
}
