using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    abstract class RepositoryFactory
    {
        public abstract Repository GetRepository();
    }
}
