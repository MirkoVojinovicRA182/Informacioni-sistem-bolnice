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

        private List<Appointment> appointments;
        AppointmentsRepository appointmentsFile;

        public AppointmentService()
        {
            // TODO: implement
            appointments = new List<Appointment>();
            appointmentsFile = new AppointmentsRepository();
        }

        ~AppointmentService()
        {
            // TODO: implement
        }

        /// <pdGenerated>default getter</pdGenerated>
        public List<Appointment> getAppointment()
        {
            if (appointments == null)
                appointments = new List<Appointment>();
            return appointments;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void setAppointment(List<Appointment> newAppointment)
        {
            removeAllAppointment();
            foreach (Appointment oAppointment in newAppointment)
                addAppointment(oAppointment);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void addAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointments == null)
                this.appointments = new List<Appointment>();
            if (!this.appointments.Contains(newAppointment))
            {
                this.appointments.Add(newAppointment);
                newAppointment.doctor.AddAppointment(newAppointment);
                newAppointment.patient.AddAppointment(newAppointment);
            }
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void removeAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointments != null)
                if (this.appointments.Contains(oldAppointment))
                {
                    oldAppointment.doctor.RemoveAppointment(oldAppointment);
                    oldAppointment.patient.RemoveAppointment(oldAppointment);
                    this.appointments.Remove(oldAppointment);
                }
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void removeAllAppointment()
        {
            if (appointments != null)
                appointments.Clear();
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

            foreach (Appointment appointment in appointments)
            {
                if (Object.Equals(appointment.room, room))
                    list.Add(appointment);
            }

            return list;
        }

        public List<Appointment> GetAppointmentsByPatient(Patient patient)
        {
            List<Appointment> list = new List<Appointment>();

            foreach (var appointment in appointments)
            {
                if (appointment.patient.Jmbg == patient.Jmbg)
                    list.Add(appointment);
            }

            return list;
        }

        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            List<Appointment> list = new List<Appointment>();

            foreach (var appointment in appointments)
            {
                if (appointment.doctor.Name == doctor.Name)
                    list.Add(appointment);
            }

            return list;
        }

        public void ChangeStartTime(Appointment appointmentForChange, DateTime newStartTime)
        {
            appointmentForChange.StartTime = newStartTime;
        }

        public DateTime GetStartTime(Appointment appointment)
        {
            return appointment.StartTime;
        }

        public Doctor GetDoctor(Appointment appointment)
        {
            return appointment.doctor;
        }

        public void saveInFile()
        {
            appointmentsFile.saveInFile();
        }

        public void loadFromFile()
        {
            appointmentsFile.loadFromFile();
        }
        public void DeleteAllAppointmentsFromRoom(Room room)
        {
            foreach (Appointment appointmentForRemove in findAppointmentByRoom(room))
            {
                removeAppointment(appointmentForRemove);
            }
        }
    }
}