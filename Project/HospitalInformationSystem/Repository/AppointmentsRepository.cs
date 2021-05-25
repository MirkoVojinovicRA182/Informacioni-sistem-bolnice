/***********************************************************************
 * Module:  DoctorAppointmentsFIleManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Repository.DoctorAppointmentsFIleManipulation
 ***********************************************************************/

using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HospitalInformationSystem.Repository
{
    public class AppointmentsRepository : IRepository
    {
        private List<Appointment> _allAppointments;
        public AppointmentsRepository()
        {
            _allAppointments = new List<Appointment>();
        }
        public void saveInFile()
        {
            // TODO: implement
            FileStream fs = new FileStream("Appointments.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, _allAppointments);
            }
            catch (SerializationException e)
            {
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
        public void loadFromFile()
        {
            if (File.Exists("Appointments.dat"))
            {
                FileStream fs = new FileStream("Appointments.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    AppointmentController.getInstance().SetAppointments((List<Appointment>)formatter.Deserialize(fs));
                }
                catch (SerializationException e)
                {
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }
        public void AddAppointmentToAppointmentList(Appointment newAppointment)
        {
            _allAppointments.Add(newAppointment);
        }
        public List<Appointment> GetAppointments()
        {
            if (_allAppointments == null)
                _allAppointments = new List<Appointment>(); 
            return _allAppointments;
        }
        public void SetAppointments(List<Appointment> newAppointments)
        {
            _allAppointments.Clear();
            foreach (Appointment appointment in newAppointments)
                _allAppointments.Add(appointment);
        }
        public void DeleteAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (_allAppointments != null)
                if (_allAppointments.Contains(oldAppointment))
                {
                    oldAppointment.doctor.RemoveAppointment(oldAppointment);
                    oldAppointment.patient.RemoveAppointment(oldAppointment);
                    this._allAppointments.Remove(oldAppointment);
                }
        }
        public List<Appointment> GetAppointmentsByPatient(Patient patient)
        {
            List<Appointment> list = new List<Appointment>();

            foreach (var appointment in _allAppointments)
            {
                if (appointment.patient.Jmbg == patient.Jmbg)
                    list.Add(appointment);
            }

            return list;
        }
        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            List<Appointment> list = new List<Appointment>();

            foreach (var appointment in _allAppointments)
            {
                if (appointment.doctor.Name == doctor.Name)
                    list.Add(appointment);
            }

            return list;
        }

        public List<Appointment> FindAppointmentByRoom(Room room)
        {
            List<Appointment> list = new List<Appointment>();

            foreach (Appointment appointment in _allAppointments)
            {
                if (Object.Equals(appointment.room, room))
                    list.Add(appointment);
            }

            return list;
        }
    }
}