using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public class AppointmentGenerationContext
    {
        private IAppointmentGeneration _strategy;
        public void SetStrategy(IAppointmentGeneration strategy)
        {
            _strategy = strategy;
        }
        public List<Appointment> RecommendAppointments(Doctor selectedDoctor, Patient patient, DateTime startDateTime, DateTime endDateTime)
        {
            return _strategy.GenerateAppointments(selectedDoctor, patient, startDateTime, endDateTime);
        }
    }
}
