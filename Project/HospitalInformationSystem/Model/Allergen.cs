using HospitalInformationSystem.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Allergen
    {
        public int ID 
        { get; set; }
        public string Name
        { get; set; }
      
        public bool isAllergic
        { get; set; }

        public Allergen(string name) 
        {
            Name = name;
            isAllergic = false;
        }
    }
}
