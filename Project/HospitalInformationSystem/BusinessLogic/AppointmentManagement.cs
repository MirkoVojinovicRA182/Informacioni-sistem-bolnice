using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace HospitalInformationSystem.BusinessLogic
{
    class AppointmentManagement
    {

        public AppointmentManagement() { }

        public void createAppointment(System.DateTime startTime, TypeOfAppointment typeOfAppointment, Room room, Patient patient, Doctor doctor)
        {
            // TODO: implement
            Appointment appointment = new Appointment(startTime, typeOfAppointment, room, patient, doctor);

            AppointmentDataBase.getInstance().AddAppointment(appointment);
        }

        public void deleteAppointment(Appointment appointment)
        {
            // TODO: implement
            AppointmentDataBase.getInstance().RemoveAppointment(appointment);
        }

        public void deleteAllAppointments()
        {
            // TODO: implement
            AppointmentDataBase.getInstance().RemoveAllAppointment();
        }

        public void changeAppointment(Appointment appointment, System.DateTime startTime, TypeOfAppointment typeOfAppointment, Room room, Patient patient, Doctor doctor)
        {
            // TODO: implement
            appointment.StartTime = startTime;
            appointment.Type = typeOfAppointment;
            appointment.room = room;
            appointment.patient = patient;
            appointment.doctor = doctor;
        }

        public List<Appointment> findAppointmentByRoom(Room room)
        {
            List<Appointment> list = new List<Appointment>();

            foreach(Appointment appointment in AppointmentDataBase.getInstance().GetAppointment())
            {
                if (Object.Equals(appointment.room,room))
                    list.Add(appointment);
            }

            return list;
        }
    }
}
