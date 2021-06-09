using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public interface IAppointmentGeneration
    {
        List<Appointment> GenerateAppointments(Doctor selectedDoctor, Patient patient, DateTime startDateTime, DateTime endDateTime);
    }
}
