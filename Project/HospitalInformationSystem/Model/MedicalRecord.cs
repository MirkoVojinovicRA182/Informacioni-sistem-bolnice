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
        private List<Anamnesis> anamnesisList;
        private List<Prescription> prescriptionList;

        public MedicalRecord()
        {
            
        }

        public MedicalRecord(int id)
        {
            this.id = id;
            anamnesisList = new List<Anamnesis>();
            prescriptionList = new List<Prescription>();
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

        public void addPrescription(Prescription prescription)
        {
            prescriptionList.Add(prescription);
        }

        public List<Prescription> getPrescriptions()
        {
            return prescriptionList;
        }
    }
}
