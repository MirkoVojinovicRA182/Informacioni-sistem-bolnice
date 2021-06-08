using Model;
using System;

namespace HospitalInformationSystem.Model
{
    [Serializable]
    public class HospitalTreatment : IHospitalResidence
    {
        public HospitalTreatment(DateTime treatmentStartDate, DateTime treatmentEndDate, Room treatmentRoom)
        {
            this.TreatmentStartDate = treatmentStartDate;
            this.TreatmentEndDate = treatmentEndDate;
            this.TreatmentRoom = treatmentRoom;
        }
        public DateTime TreatmentStartDate { get; set; }
        public DateTime TreatmentEndDate { get; set; }
        public Room TreatmentRoom { get; set; }
        public void ChangeResidenceDate(DateTime treatmentStartDate, DateTime treatmentEndDate)
        {
            this.TreatmentStartDate = treatmentStartDate;
            this.TreatmentEndDate = treatmentEndDate;
        }

        public void ChangeResidence(IHospitalResidence newResidence)
        {
            this.TreatmentStartDate = ((HospitalTreatment)newResidence).TreatmentStartDate;
            this.TreatmentEndDate = ((HospitalTreatment)newResidence).TreatmentEndDate;
            this.TreatmentRoom = ((HospitalTreatment)newResidence).TreatmentRoom;
        }
    }
}
