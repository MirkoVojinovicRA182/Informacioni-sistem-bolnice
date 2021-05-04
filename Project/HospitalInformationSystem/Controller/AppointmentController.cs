using Model;
using HospitalInformationSystem.Service;
using System.Collections.Generic;
using System;

namespace HospitalInformationSystem.Controller
{
    class AppointmentController
    {
        AppointmentService appointmentService;

        private static AppointmentController instance = null;

        public static AppointmentController getInstance()
        {
            if (instance == null)
                instance = new AppointmentController();
            return instance;
        }

        private AppointmentController()
        {
            appointmentService = new AppointmentService();
        }

        public List<Appointment> getAppointment()
        {
            return appointmentService.getAppointment();
        }

        public void setAppointment(List<Appointment> newAppointment)
        {
            appointmentService.setAppointment(newAppointment);
        }

        public void addAppointment(Appointment newAppointment)
        {
            appointmentService.addAppointment(newAppointment);
        }

        public void removeAppointment(Appointment oldAppointment)
        {
            appointmentService.removeAppointment(oldAppointment);
        }

        public void removeAllAppointment()
        {
            appointmentService.removeAllAppointment();
        }

        public void changeAppointment(Appointment appointment, System.DateTime startTime, TypeOfAppointment typeOfAppointment, Room room, Patient patient, Doctor doctor)
        {
            appointmentService.changeAppointment(appointment, startTime, typeOfAppointment, room, patient, doctor);
        }

        public List<Appointment> findAppointmentByRoom(Room room)
        {
            return appointmentService.findAppointmentByRoom(room);
        }

        public void ChangeStartTime(Appointment appointmentForChange, DateTime newStartTime)
        {
            appointmentService.ChangeStartTime(appointmentForChange, newStartTime);
        }

        public DateTime GetStartTime(Appointment appointment)
        {
            return appointmentService.GetStartTime(appointment);
        }

        public Doctor GetDoctor(Appointment appointment)
        {
            return appointmentService.GetDoctor(appointment);
        }

        public void saveInFile()
        {
            appointmentService.saveInFile();
        }

        public void loadFromFile()
        {
            appointmentService.loadFromFile();
        }

        public List<Appointment> GetAppointmentsByPatient(Patient patient)
        {
            return appointmentService.GetAppointmentsByPatient(patient);
        }
    }
}
