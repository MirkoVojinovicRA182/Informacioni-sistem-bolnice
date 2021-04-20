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
        List<Anamnesis> anamnesisList;

        public MedicalRecord()
        {
            
        }

        public MedicalRecord(int id)
        {
            this.id = id;
            anamnesisList = new List<Anamnesis>();
        }

        public int getId()
        {
            return id;
        }

        public void addAnamnesis(Anamnesis anamnesis)
        {
            anamnesisList.Add(anamnesis);
        }

        public List<Anamnesis> getAnamneses()
        {
            return anamnesisList;
        }
    }
}
