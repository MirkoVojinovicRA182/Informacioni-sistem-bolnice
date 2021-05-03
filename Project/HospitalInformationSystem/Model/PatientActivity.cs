using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public class PatientActivity
    {
        public PatientActivity(int finishedAppointments, int numberOfMovedAppointmentsInMonth, int numberOfScheduledAppointmentsInDay, bool isTroll)
        {
            NumberOfFinishedAppointmentsSinceReview = finishedAppointments;
            NumberOfMovedAppointmentsInMonth = numberOfMovedAppointmentsInMonth;
            NumberOfScheduledAppointmentsInDay = numberOfScheduledAppointmentsInDay;
            IsTroll = isTroll;
        }

        public int NumberOfFinishedAppointmentsSinceReview
        {
            get; set;
        }
        public int NumberOfMovedAppointmentsInMonth
        {
            get; set;
        }

        public int NumberOfScheduledAppointmentsInDay
        {
            get; set;
        }

        public bool IsTroll
        {
            get; set;
        }

    }
}