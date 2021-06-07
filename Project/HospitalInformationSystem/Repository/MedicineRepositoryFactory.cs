using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    class MedicineRepositoryFactory : RepositoryFactory
    {
        private List<Medicine> collection;
        public override Repository GetRepository()
        {
            return MedicineRepository();
        }
    }
}
