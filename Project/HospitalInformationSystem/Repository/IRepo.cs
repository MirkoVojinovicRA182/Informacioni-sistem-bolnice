using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    public interface IRepo
    {
        void AddNew(Object element);
        void DeleteOne(Object element);
        bool Exists(int id);
        void Serialize();
        void Deserialize();
    }
}
