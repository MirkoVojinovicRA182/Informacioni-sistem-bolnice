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
        public PatientActivity(int finishedAppointments)
        {
            NumberOfFinishedAppointmentsSinceReview = finishedAppointments;
        }

        public int NumberOfFinishedAppointmentsSinceReview
        {
            get; set;
        }
    }
}