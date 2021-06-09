using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public class AppointmentGenerationTimePriority : IAppointmentGeneration
    {
        public List<Appointment> GenerateAppointments(Doctor selectedDoctor, Patient patient, DateTime startDateTime, DateTime endDateTime) 
        {
            List<Appointment> recommendedAppointments = AppointmentController.getInstance().GenerateFreeAppointments(selectedDoctor, patient, startDateTime.AddDays(-3), endDateTime.AddDays(3));

            if (!recommendedAppointments.Any())
                recommendedAppointments = AppointmentController.getInstance().GenerateAppointmentsForDoctorsOfSameSpecialization(selectedDoctor, patient, startDateTime, endDateTime);

            return recommendedAppointments;
        }
    }
}
