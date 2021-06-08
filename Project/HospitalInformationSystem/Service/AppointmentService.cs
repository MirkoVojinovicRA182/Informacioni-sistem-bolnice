/***********************************************************************
 * Module:  AppointmentDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.AppointmentDataBase
 ***********************************************************************/

using Model;
using HospitalInformationSystem.Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace HospitalInformationSystem.Service
{
    public class AppointmentService
    {
        private AppointmentsRepository _repository;
        public AppointmentService()
        {
            _repository = new AppointmentsRepository();
        }
        public void AddAppointmentToAppointmentList(Appointment newAppointment)
        {
            _repository.AddAppointmentToAppointmentList(newAppointment);
        }
        public List<Appointment> GetAppointments()
        {
            return _repository.GetAppointments();
        }
        public void SetAppointments(List<Appointment> appointments)
        {
            _repository.SetAppointments(appointments);
        }
        public void DeleteAppointment(Appointment appointment)
        {
            _repository.DeleteAppointment(appointment);
        }
        public void ChangeAppointment(Appointment appointment, Appointment newAppointment)
        {
            appointment.ChangeResidence(newAppointment);
        }
        public List<Appointment> FindAppointmentByRoom(Room room)
        {
            return _repository.FindAppointmentByRoom(room);
        }
        public List<Appointment> GetAppointmentsByPatient(Patient patient)
        {
            return _repository.GetAppointmentsByPatient(patient);
        }
        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            return _repository.GetAppointmentsByDoctor(doctor);
        }
        public void SaveAppointmentsInFile()
        {
            _repository.saveInFile();
        }
        public void LoadAppointmentsFromFile()
        {
            _repository.loadFromFile();
        }
        public void DeleteAllAppointmentsFromRoom(Room room)
        {
            foreach (Appointment appointmentForRemove in FindAppointmentByRoom(room))
            {
                DeleteAppointment(appointmentForRemove);
            }
        }
        public void RemoveAllAppointments()
        {
            if (GetAppointments() != null)
                GetAppointments().Clear();
        }

        public bool AppointmentIsTaken(Appointment appointment)
        {
            bool isTaken = false;
            foreach (var currentAppointment in _repository.GetAppointments())
            {
                if (currentAppointment.Doctor == appointment.Doctor && currentAppointment.StartTime == appointment.StartTime)
                    return true;
            }
            return isTaken;
        }
    }
}