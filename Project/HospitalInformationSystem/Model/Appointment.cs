/***********************************************************************
 * Module:  Appointment.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Appointment
 ***********************************************************************/

using HospitalInformationSystem.Model;
using System;

namespace Model
{
    [Serializable]
    public class Appointment : IHospitalResidence
    {
        public Appointment()
        {
            // TODO: implement
        }

        public Appointment(DateTime date, TypeOfAppointment type, Room room, Patient patient, Doctor doctor)
        {
            this.StartTime = date;
            this.Type = type;
            this.Room = room;
            this.Patient = patient;
            this.Doctor = doctor;
        }

        public Appointment(DateTime date, TypeOfAppointment type, Room room, Patient patient)
        {
            this.StartTime = date;
            this.Type = type;
            this.Room = room;
            this.Patient = patient;
        }

        public Appointment(DateTime startTime, Doctor doctor, TypeOfAppointment type)
        {
            StartTime = startTime;
            this.Doctor = doctor;
            this.Type = type;
        }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }

        public Room Room { get; set; }

        public DateTime StartTime { get; set; }

        public TypeOfAppointment Type { get; set; }

        public int GetRoomID
        {
            get
            {
                return Room.Id;
            }
            set { }
        }

        public string GetPatientNameSurname
        {
            get
            {
                return Patient.Name + " " + Patient.Surname;
            }
            set { }
        }

        public string GetDoctorNameSurname
        {
            get
            {
                return Doctor.Name + " " + Doctor.Surname;
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

        public void ChangeResidence(IHospitalResidence newResidence)
        {
            this.Doctor = ((Appointment)newResidence).Doctor;
            this.Patient = ((Appointment)newResidence).Patient;
            this.Room = ((Appointment)newResidence).Room;
            this.StartTime = ((Appointment)newResidence).StartTime;
            this.Type = ((Appointment)newResidence).Type;
        }
    }
}