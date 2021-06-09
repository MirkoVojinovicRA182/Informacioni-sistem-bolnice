using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    public interface IFactory
    {
        void Serialization();
        ObservableCollection<object> LoadAll();
    }
}
