
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

        private MedicalRecord medicalRecord;

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
        }

        public Patient(string name, string surname, string username, List<Therapy> therapy)
        {
            this.Name = name;
            this.Surname = surname;
            this.Username = username;
            this.therapy = therapy;
            this.Allergens = new List<string>();
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
            this.medicalRecord = new MedicalRecord();
        }

        public Patient(string name, string surname, PatientActivity activity)
        {
            Name = name;
            Surname = surname;
            Activity = activity;
            this.Allergens = new List<string>();
        }

        public List<Appointment> appointment;
        private List<Therapy> therapy;

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

        public List<Therapy> GetTherapy()
        {
            if (therapy == null)
                therapy = new List<Therapy>();
            return therapy;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetTherapy(List<Therapy> newTherapy)
        {
            RemoveAllTherapy();
            foreach (Therapy oTherapy in newTherapy)
                AddTherapy(oTherapy);
        }

        public void AddTherapy(Therapy newTherapy)
        {
            if (newTherapy == null)
                return;
            if (this.therapy == null)
                this.therapy = new List<Therapy>();
            if (!this.therapy.Contains(newTherapy))
            {
                this.therapy.Add(newTherapy);
            }
        }

        public void RemoveAllTherapy()
        {
            if (therapy != null)
            {
                List<Therapy> tmpTherapy = new List<Therapy>();
                foreach (Therapy oldTherapy in therapy)
                    tmpTherapy.Add(oldTherapy);
                therapy.Clear();
                tmpTherapy.Clear();
            }
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
            get { return medicalRecord.getId(); }
            set { }
        }

        public HospitalTreatment hospitalTreatment { get; set; }

        public List<string> Allergens { get; set; }
    }
}