using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    public class MedRepository : Repository
    {
        List<object> medicines;
        public override List<object> Collection {
            get { return medicines; }
            set { }
        }
        public MedRepository()
        {
            medicines = new List<object>(new List<Medicine>());
        }
    }
}
