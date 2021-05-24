/***********************************************************************
 * Module:  PatientManagement.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Service.PatientManagement
 ***********************************************************************/

using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using HospitalInformationSystem.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Model.Patient;

namespace HospitalInformationSystem.Service
{
    public class PatientService
    {
        PatientsRepository patientsFile;
        public ObservableCollection<Patient> patients;
        public ObservableCollection<Allergen> allergens;

        public PatientService()
        {
            patients = new ObservableCollection<Patient>();
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

        public ObservableCollection<Allergen> getAllergens()
        {
            if (allergens == null)
                allergens = new ObservableCollection<Allergen>();
            return allergens;
        }

        public void setAllergens(ObservableCollection<Allergen> allergenList)
        {
            allergens = allergenList;
        }

        public void addAllergen(Allergen newAllergen)
        {
            if (newAllergen == null)
                return;
            if (this.allergens == null)
                this.allergens = new ObservableCollection<Allergen>();

            foreach (Allergen a in this.allergens)
            {
                if (a.Name == newAllergen.Name)
                    return;
            }
            this.allergens.Add(newAllergen);
        }

        /// <pdGenerated>default getter</pdGenerated>
        public ObservableCollection<Patient> getPatient()
        {
            if (patients == null)
                patients = new ObservableCollection<Patient>();
            return patients;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void setPatient(ObservableCollection<Patient> newPatient)
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
                this.patients = new ObservableCollection<Patient>();
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
            patient.MedicalRecord.addPrescription(prescription);
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
            if (patientToAddAllergen.Allergens == null)
                patientToAddAllergen.Allergens = new List<string>();
            patientToAddAllergen.Allergens.Add(allergen);
        }

        public List<Patient> GetPatientsOnHospitalTretment()
        {
            List<Patient> patientsOnHospitalTretment = new List<Patient>();
            foreach(Patient patient in PatientController.getInstance().getPatient())
            {
                if (patient.hospitalTreatment != null) patientsOnHospitalTretment.Add(patient);
            }
            return patientsOnHospitalTretment;
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