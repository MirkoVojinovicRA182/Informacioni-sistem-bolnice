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
        public Doctor()
        {
            // TODO: implement
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
            // TODO: implement
            this.Name = name;
            this.Surname = surname;
            this.Specialization = specialization;
            this.room = room;
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
            RemoveAllAppointment();
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
                newAppointment.SetDoctor(this);
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
                    oldAppointment.SetDoctor((Doctor)null);
                }
        }

        public void RemoveAllAppointment()
        {
            if (appointments != null)
            {
                System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
                foreach (Appointment oldAppointment in appointments)
                    tmpAppointment.Add(oldAppointment);
                appointments.Clear();
                foreach (Appointment oldAppointment in tmpAppointment)
                    oldAppointment.SetDoctor((Doctor)null);
                tmpAppointment.Clear();
            }
        }
        public Room room
        {
            get; set;
        }

        public Specialization Specialization
        {
            get; set;
        }


    }
}