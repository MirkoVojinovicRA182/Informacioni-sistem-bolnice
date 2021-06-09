
/***********************************************************************
 * Module:  Patient.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Patient : Person
    {
        public enum BloodType
        {
            A,
            B
        }

        public bool IsGuest
        { get; set; }
        public BloodType Blood
        { get; set; }
        public string LBO
        { get; set; }
        public PatientActivity Activity
        {
            get; set;
        }

        public MedicalRecord MedicalRecord { get; set; }

        public Patient()
        {
            // TODO: implement
        }

        ~Patient()
        {
            // TODO: implement
        }

        public Patient(string name, string surname, string username)
        {
            this.Name = name;
            this.Surname = surname;
            this.Username = username;
            this.Allergens = new List<string>();
            DoctorReviews = new List<DoctorReview>();
            HospitalReviews = new List<HospitalReview>();
        }

        public Patient(string username, string name, string surname,
    DateTime dateOfBirth, string phoneNumber, string email, string parentsName,
    string gender, string jmbg, bool isGuest, BloodType blood, string lbo)
        {
            Username = username;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            ParentsName = parentsName;
            Gender = gender;
            Jmbg = jmbg;
            IsGuest = isGuest;
            Blood = blood;
            LBO = lbo;
            this.Allergens = new List<string>();
            this.MedicalRecord = new MedicalRecord();
            this.Activity = new PatientActivity();
            DoctorReviews = new List<DoctorReview>();
            HospitalReviews = new List<HospitalReview>();
        }

        public Patient(string name, string surname, PatientActivity activity)
        {
            Name = name;
            Surname = surname;
            Activity = activity;
            this.Allergens = new List<string>();
            DoctorReviews = new List<DoctorReview>();
            HospitalReviews = new List<HospitalReview>();
        }

        public List<Appointment> appointment;

        /// <pdGenerated>default getter</pdGenerated>
        public List<Appointment> GetAppointment()
        {
            if (appointment == null)
                appointment = new List<Appointment>();
            return appointment;
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
            if (this.appointment == null)
                this.appointment = new List<Appointment>();
            if (!this.appointment.Contains(newAppointment))
            {
                this.appointment.Add(newAppointment);
                newAppointment.Patient = this;
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
                    oldAppointment.Patient = null;
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
                    oldAppointment.Patient = null;
                tmpAppointment.Clear();
            }
        }

        public void setMedicalRecord(MedicalRecord medicalRecord)
        {
            this.MedicalRecord = medicalRecord;
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }

        public string GetPatientNameAndSurname
        {
            get { return Name + " " + Surname; }
            set { }
        }

        public int GetMedicalRecordId
        {
            get { return MedicalRecord.getId(); }
            set { }
        }

        public HospitalTreatment hospitalTreatment { get; set; }

        public List<string> Allergens { get; set; }

        public List<DoctorReview> DoctorReviews { get; set; }
        public List<HospitalReview> HospitalReviews { get; set; }
    }
}