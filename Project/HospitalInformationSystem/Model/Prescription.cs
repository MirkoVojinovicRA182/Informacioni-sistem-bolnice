using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Prescription
    {
        public Prescription()
        {

        }

        public Prescription(Medicine medicine, DateTime startTime, DateTime endTime, string info)
        {
            this.medicine = medicine;
            this.startTime = startTime;
            this.endTime = endTime;
            this.info = info;
        }
        public String prescriptionInfo
        {
            get { return medicine.Name + " " + startTime.ToString("dd.MM.yyyy."); }

            set { }
        }
        public Medicine medicine
        {
            get;

            set;
        }

        public DateTime startTime
        {
            get;

            set;
        }

        public DateTime endTime
        {
            get;

            set;
        }

        public string info
        {
            get;

            set;
        }
    }
}
