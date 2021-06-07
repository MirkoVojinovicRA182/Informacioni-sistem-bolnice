using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    public abstract class Repository
    {
        public abstract List<Object> Collection { get; set; }
    }
}
