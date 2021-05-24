using System;
namespace HospitalInformationSystem.Model
{
    [Serializable]
    public class RoomRenovationState
    {
        public RoomRenovationState(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        public DateTime StartDate
        {
            get; set;
        }
        public DateTime EndDate
        {
            get; set;
        }
        public bool ActivityStatus
        {
            get; set;
        }
    }

}
