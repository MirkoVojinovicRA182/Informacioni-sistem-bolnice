/***********************************************************************
 * Module:  Appointment.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Appointment
 ***********************************************************************/

using System;

namespace Model
{
    [Serializable]
    public class Appointment
    {
        public Appointment()
        {
            // TODO: implement
        }

        public Appointment(System.DateTime date, TypeOfAppointment type, Room room, Patient patient, Doctor doctor)
        {
            this.StartTime = date;
            this.Type = type;
            this.room = room;
            this.patient = patient;
            this.doctor = doctor;
        }

        public Appointment(System.DateTime date, TypeOfAppointment type, Room room, Patient patient)
        {
            this.StartTime = date;
            this.Type = type;
            this.room = room;
            this.patient = patient;
        }

        public Appointment(System.DateTime startTime, Doctor doctor, TypeOfAppointment type)
        {
            StartTime = startTime;
            this.doctor = doctor;
            this.Type = type;
        }

        public Patient patient;

        /// <pdGenerated>default parent getter</pdGenerated>
        public Patient GetPatient()
        {
            return patient;
        }

        /// <pdGenerated>default parent setter</pdGenerated>
        /// <param>newPatient</param>
        public void SetPatient(Patient newPatient)
        {
            if (this.patient != newPatient)
            {
                if (this.patient != null)
                {
                    Patient oldPatient = this.patient;
                    this.patient = null;
                    oldPatient.RemoveAppointment(this);
                }
                if (newPatient != null)
                {
                    this.patient = newPatient;
                    this.patient.AddAppointment(this);
                }
            }
        }
        public Doctor doctor;

        /// <pdGenerated>default parent getter</pdGenerated>
        public Doctor GetDoctor()
        {
            return doctor;
        }

        /// <pdGenerated>default parent setter</pdGenerated>
        /// <param>newDoctor</param>
        public void SetDoctor(Doctor newDoctor)
        {
            if (this.doctor != newDoctor)
            {
                if (this.doctor != null)
                {
                    Doctor oldDoctor = this.doctor;
                    this.doctor = null;
                    oldDoctor.RemoveAppointment(this);
                }
                if (newDoctor != null)
                {
                    this.doctor = newDoctor;
                    this.doctor.AddAppointment(this);
                }
            }
        }
        public Room room;

        public System.DateTime StartTime
        {
            get; set;
        }

        public TypeOfAppointment Type
        {
            get; set;
        }

        public int GetRoomID
        {
            get
            {
                return room.Id;
            }
            set { }
        }

        public string GetPatientNameSurname
        {
            get
            {
                return patient.Name + " " + patient.Surname;
            }
            set { }
        }

        public string GetDoctorNameSurname
        {
            get
            {
                return doctor.Name + " " + doctor.Surname;
            }
            set { }
        }

        public bool HasBeenMoved
        {
            get; set;
        }
        public DateTime SchedulingTime
        {
            get; set;
        }
        public DateTime MovingTime
        {
            get; set;
        }
        public bool HasBeenReviewed
        {
            get; set;
        }
    }
}