/***********************************************************************
 * Module:  PatientManagement.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Service.PatientManagement
 ***********************************************************************/

using HospitalInformationSystem.Model;
using HospitalInformationSystem.Repository;
using Model;
using System;
using System.Collections.Generic;
using static Model.Patient;

namespace HospitalInformationSystem.Service
{
    public class PatientService
    {
        PatientsRepository patientsFile;
        public List<Patient> patients;
        public List<Allergen> allergens;

        public PatientService()
        {
            patients = new List<Patient>();
            patientsFile = new PatientsRepository();
        }

        public void CreatePatient(string username, string name, string surname,
            DateTime dateOfBirth, string phoneNumber, string email, string parentsName,
            string gender, string jmbg, bool isGuest, BloodType blood, string lbo)
        {
            Patient patient = new Patient(username, name, surname,
            dateOfBirth, phoneNumber, email, parentsName,
            gender, jmbg, isGuest, blood, lbo);
            System.Console.WriteLine("NAPRAVLJEN PACIJENT :" + patient);
            patients.Add(patient);
            
        }

        public List<Allergen> getAllergens()
        {
            if (allergens == null)
                allergens = new List<Allergen>();
            return allergens;
        }

        public void setAllergens(List<Allergen> allergenList)
        {
            allergens = allergenList;
        }

        public void addAllergen(Allergen newAllergen)
        {
            if (newAllergen == null)
                return;
            if (this.allergens == null)
                this.allergens = new List<Allergen>();

            foreach (Allergen a in this.allergens)
            {
                if (a.Name == newAllergen.Name)
                    return;
            }
            this.allergens.Add(newAllergen);
        }

        /// <pdGenerated>default getter</pdGenerated>
        public List<Patient> getPatient()
        {
            if (patients == null)
                patients = new List<Patient>();
            return patients;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void setPatient(List<Patient> newPatient)
        {
            RemoveAllPatient();
            foreach (Patient oPatient in newPatient)
                AddPatient(oPatient);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddPatient(Patient newPatient)
        {
            if (newPatient == null)
                return;
            if (this.patients == null)
                this.patients = new List<Patient>();
            if (!this.patients.Contains(newPatient))
                this.patients.Add(newPatient);


        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemovePatient(Patient oldPatient)
        {
            if (oldPatient == null)
                return;
            if (this.patients != null)
                if (this.patients.Contains(oldPatient))
                    this.patients.Remove(oldPatient);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllPatient()
        {
            if (patients != null)
                patients.Clear();
        }

        public void AddPrescription(Patient patient, Prescription prescription)
        {
            patient.GetMedicalRecord().addPrescription(prescription);
        }

        public void AddHospitalTreatment(Patient patientToSendOnHospitalTreatmant, HospitalTreatment hospitalTreatment)
        {
            patientToSendOnHospitalTreatmant.hospitalTreatment = hospitalTreatment;
        }

        public void EditHospitalTreatment(Patient patientToEditHospitalTreatment, DateTime startTime, DateTime endTime, Room roomForTreatment)
        {
            patientToEditHospitalTreatment.hospitalTreatment.treatmentStartDate = startTime;
            patientToEditHospitalTreatment.hospitalTreatment.treatmentEndDate = endTime;
            patientToEditHospitalTreatment.hospitalTreatment.treatmentRoom = roomForTreatment;
        }

        public void AddAllergenToPatient(Patient patientToAddAllergen, string allergen)
        {
            patientToAddAllergen.Allergens.Add(allergen);
        }
        public void SaveInFile()
        {
            patientsFile.saveInFile();
        }

        public void LoadFromFile()
        {
            patientsFile.loadFromFile();
        }
    }
}