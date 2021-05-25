using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    [Serializable]
    public class Notification
    {
        public Notification(string contents, DateTime time, DateTime startDate, DateTime endDate, bool isEnabled)
        {
            Contents = contents;
            Time = time;
            StartDate = startDate;
            EndDate = endDate;
            IsEnabled = isEnabled;
        }

        public string Contents
        {
            get; set;
        }

        public DateTime Time
        {
            get; set;
        }

        public DateTime StartDate
        {
            get; set;
        }

        public DateTime EndDate
        {
            get; set;
        }

        public bool IsEnabled
        {
            get; set;
        }

        public Patient Patient
        {
            get; set;
        }
    }
}
