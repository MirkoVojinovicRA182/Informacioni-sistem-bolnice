using Model;
using System;

namespace HospitalInformationSystem.Model
{
    [Serializable]
    public class HospitalTreatment
    {
        public HospitalTreatment(DateTime treatmentStartDate, DateTime treatmentEndDate, Room treatmentRoom)
        {
            this.treatmentStartDate = treatmentStartDate;
            this.treatmentEndDate = treatmentEndDate;
            this.treatmentRoom = treatmentRoom;
        }
        public DateTime treatmentStartDate { get; set; }
        public DateTime treatmentEndDate { get; set; }
        public Room treatmentRoom { get; set; }
    }
}
