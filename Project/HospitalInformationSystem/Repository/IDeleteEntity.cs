using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    public interface IDeleteEntity
    {
        void PermanentlyDelete(object entity);
    }
}
