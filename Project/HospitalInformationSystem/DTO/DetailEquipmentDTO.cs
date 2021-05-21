using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.DTO
{
    class DetailEquipmentDTO
    {
        public DetailEquipmentDTO(string name, string type, int state, string location)
        {
            Name = name;
            Type = type;
            State = state;
            Location = location;
        }
        public string Name
        {
            get; set;
        }
        public string Type
        {
            get; set;
        }
        public int State
        {
            get; set;
        }
        public string Location
        {
            get; set;
        }

    }
}
