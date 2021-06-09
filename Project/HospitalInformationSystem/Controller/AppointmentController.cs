using Model;
using HospitalInformationSystem.Service;
using System.Collections.Generic;
using System;

namespace HospitalInformationSystem.Controller
{
    class AppointmentController
    {
        private AppointmentService _appointmentService;
        private static AppointmentController _instance = null;
        public static AppointmentController getInstance()
        {
            if (_instance == null)
                _instance = new AppointmentController();
            return _instance;
        }
        private AppointmentController()
        {
            _appointmentService = new AppointmentService();
        }
        public List<Appointment> GetAppointments()
        {
            return _appointmentService.GetAppointments();
        }
        public void SetAppointments(List<Appointment> newAppointment)
        {
            _appointmentService.SetAppointments(newAppointment);
        }
        public void AddAppointmentToAppointmentList(Appointment newAppointment)
        {
            _appointmentService.AddAppointmentToAppointmentList(newAppointment);
        }
        public void DeleteAppointment(Appointment oldAppointment)
        {
            _appointmentService.DeleteAppointment(oldAppointment);
        }
        public void ChangeAppointment(Appointment appointment, System.DateTime startTime, TypeOfAppointment typeOfAppointment, Room room, Patient patient, Doctor doctor)
        {
            _appointmentService.ChangeAppointment(appointment, startTime, typeOfAppointment, room, patient, doctor);
        }
        public List<Appointment> FindAppointmentByRoom(Room room)
        {
            return _appointmentService.FindAppointmentByRoom(room);
        }
        public void SaveAppointmentsInFile()
        {
            _appointmentService.SaveAppointmentsInFile();
        }
        public void LoadAppointmentsFromFile()
        {
            _appointmentService.LoadAppointmentsFromFile();
        }
        public List<Appointment> GetAppointmentsByPatient(Patient patient)
        {
            return _appointmentService.GetAppointmentsByPatient(patient);
        }
        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            return _appointmentService.GetAppointmentsByDoctor(doctor);
        }
        public void DeleteAllAppointmentsFromRoom(Room room)
        {
            _appointmentService.DeleteAllAppointmentsFromRoom(room);
        }
        public void RemoveAllAppointments()
        {
            _appointmentService.RemoveAllAppointments();
        }
        public bool AppointmentIsTaken(Appointment appointment)
        {
            return _appointmentService.AppointmentIsTaken(appointment);
        }
        public List<Appointment> GenerateFreeAppointments(Doctor doctor, Patient patient, DateTime startDateTime, DateTime endDateTime)
        {
            return _appointmentService.GenerateFreeAppointments(doctor, patient, startDateTime, endDateTime);
        }
        public List<Appointment> GenerateAllPossibleAppointments(Doctor doctor, Patient patient, List<DateTime> dateTimes)
        {
            return _appointmentService.GenerateAllPossibleAppointments(doctor, patient, dateTimes);
        }
        private void RemoveExistingAppointments(List<Appointment> existingAppointments, List<Appointment> recommendedAppointments)
        {
            _appointmentService.RemoveExistingAppointments(existingAppointments, recommendedAppointments);
        }
        public List<Appointment> GenerateAppointmentsForDoctorsOfSameSpecialization(Doctor selectedDoctor, Patient patient, DateTime startDateTime, DateTime endDateTime)
        {
            return _appointmentService.GenerateAppointmentsForDoctorsOfSameSpecialization(selectedDoctor, patient, startDateTime, endDateTime);
        }
        public void CalculateFinishedAppointments(Patient patient)
        {
            _appointmentService.CalculateFinishedAppointments(patient);
        }
        public bool AppointmentIsFinished(Appointment appointment)
        {
            return _appointmentService.AppointmentIsFinished(appointment);
        }
    }
}
