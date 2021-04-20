/***********************************************************************
 * Module:  AppointmentDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.AppointmentDataBase
 ***********************************************************************/

using System.Collections.Generic;

namespace Model
{
    public class AppointmentDataBase
    {

        public static AppointmentDataBase instance;

        private List<Appointment> appointments;

        public static AppointmentDataBase getInstance()
        {
            if (instance == null)
                instance = new AppointmentDataBase();
            return instance;
        }
        private AppointmentDataBase()
        {
            // TODO: implement
        }

        ~AppointmentDataBase()
        {
            // TODO: implement
        }

        //public System.Collections.ArrayList appointment;

        /// <pdGenerated>default getter</pdGenerated>
        public List<Appointment> GetAppointment()
        {
            if (appointments == null)
                appointments = new List<Appointment>();
            return appointments;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetAppointment(List<Appointment> newAppointment)
        {
            RemoveAllAppointment();
            foreach (Appointment oAppointment in newAppointment)
                AddAppointment(oAppointment);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointments == null)
                this.appointments = new List<Appointment>();
            if (!this.appointments.Contains(newAppointment))
                this.appointments.Add(newAppointment);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointments != null)
                if (this.appointments.Contains(oldAppointment))
                    this.appointments.Remove(oldAppointment);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllAppointment()
        {
            if (appointments != null)
                appointments.Clear();
        }

    }
}