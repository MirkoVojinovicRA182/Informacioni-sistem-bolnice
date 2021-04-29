using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    class MedicineIngredient
    {
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
