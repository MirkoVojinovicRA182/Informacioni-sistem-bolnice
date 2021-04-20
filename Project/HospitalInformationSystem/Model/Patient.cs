
/***********************************************************************
 * Module:  Patient.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using HospitalInformationSystem.Model;
using System;

namespace Model
{
    [Serializable]
    public class Patient : Person
    {

        private MedicalRecord medicalRecord;

        public Patient()
        {
            // TODO: implement
        }

        ~Patient()
        {
            // TODO: implement
        }

        public Patient(string name, string surname, string id)
        {
            this.Name = name;
            this.Surname = surname;
            this.Id = id;
        }

        public System.Collections.ArrayList appointment;

        /// <pdGenerated>default getter</pdGenerated>
        public System.Collections.ArrayList GetAppointment()
        {
            if (appointment == null)
                appointment = new System.Collections.ArrayList();
            return appointment;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetAppointment(System.Collections.ArrayList newAppointment)
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
            if (this.appointment == null)
                this.appointment = new System.Collections.ArrayList();
            if (!this.appointment.Contains(newAppointment))
            {
                this.appointment.Add(newAppointment);
                newAppointment.SetPatient(this);
            }
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointment != null)
                if (this.appointment.Contains(oldAppointment))
                {
                    this.appointment.Remove(oldAppointment);
                    oldAppointment.SetPatient((Patient)null);
                }
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllAppointment()
        {
            if (appointment != null)
            {
                System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
                foreach (Appointment oldAppointment in appointment)
                    tmpAppointment.Add(oldAppointment);
                appointment.Clear();
                foreach (Appointment oldAppointment in tmpAppointment)
                    oldAppointment.SetPatient((Patient)null);
                tmpAppointment.Clear();
            }
        }

        public void setMedicalRecord(MedicalRecord medicalRecord)
        {
            this.medicalRecord = medicalRecord;
        }

        public MedicalRecord GetMedicalRecord()
        {
            return medicalRecord;
        }

    }
}