/***********************************************************************
 * Module:  Doctor.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Doctor
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Doctor : Person
    {
        public Room room
        {
            get; set;
        }

        public Specialization Specialization
        {
            get; set;
        }

        public Doctor()
        {
        }

        public Doctor(string name, string surname, string username, Room room, Specialization specialization)
        {
            this.Name = name;
            this.Surname = surname;
            this.Username = username;
            this.room = room;
            this.Specialization = specialization;
        }

        public Doctor(string name, string surname, Specialization specialization, Room room)
        {
            this.Name = name;
            this.Surname = surname;
            this.Specialization = specialization;
            this.room = room;
        }

        public Doctor(string username, string name, string surname, DateTime dateOfBirth, string phoneNumber, string email, 
                    string parentsname, string gender, string jmbg, Room room, Specialization specialization)
        {
            Username = username;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            ParentsName = parentsname;
            Gender = gender;
            Jmbg = jmbg;
            this.room = room;
            Specialization = specialization;
        }

        public List<Appointment> appointments;

        public List<Appointment> GetAppointment()
        {
            if (appointments == null)
                appointments = new List<Appointment>();
            return appointments;
        }

        public void SetAppointment(List<Appointment> newAppointment)
        {
            RemoveAllAppointments();
            foreach (Appointment oAppointment in newAppointment)
                AddAppointment(oAppointment);
        }

        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointments == null)
                this.appointments = new List<Appointment>();
            if (!this.appointments.Contains(newAppointment))
            {
                this.appointments.Add(newAppointment);
                newAppointment.Doctor = this;
            }
        }

        public void RemoveAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointments != null)
                if (this.appointments.Contains(oldAppointment))
                {
                    this.appointments.Remove(oldAppointment);
                    oldAppointment.Doctor = null;
                }
        }

        public void RemoveAllAppointments()
        {
            if (appointments != null)
            {
                System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
                foreach (Appointment oldAppointment in appointments)
                    tmpAppointment.Add(oldAppointment);
                appointments.Clear();
                foreach (Appointment oldAppointment in tmpAppointment)
                    oldAppointment.Doctor = null;
                tmpAppointment.Clear();
            }
        }

    }
}