using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{

    public enum BloodType
    {
        AB_Positive,
        AB_Negative,
        A_Positive,
        A_Negative,
        B_Positive,
        B_Negative,
        Zero_Positive,
        Zero_Negative,
    }
    [Serializable]
    public class MedicalRecord
    {

        private int id;
        private BloodType bloodType;
        private Patient patient;

        public MedicalRecord()
        {

        }

        public MedicalRecord(Patient patient, int id, BloodType bloodType)
        {
            this.patient = patient;
            this.bloodType = bloodType;
            this.id = id;
        }
    }
}
