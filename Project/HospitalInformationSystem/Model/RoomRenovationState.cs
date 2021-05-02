using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
