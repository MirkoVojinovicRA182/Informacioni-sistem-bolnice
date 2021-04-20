using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{

    [Serializable]
    public class MedicalRecord
    {

        private int id;

        public MedicalRecord()
        {
            List<Anamnesis> anamnesisList;
        }

        public MedicalRecord(int id)
        {
            this.id = id;
        }
    }
}
