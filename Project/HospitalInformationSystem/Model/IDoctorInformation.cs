using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public interface IDoctorInformation
    {
        Doctor Doctor
        {
            get; set;
        }
    }
}
