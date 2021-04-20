/***********************************************************************
 * Module:  PatientDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.PatientDataBase
 ***********************************************************************/

using System.Collections.Generic;

namespace Model
{
    public class PatientDataBase
    {

        public static PatientDataBase instance;

        public static PatientDataBase getInstance()
        {
            if (instance == null)
                instance = new PatientDataBase();
            return instance;
        }

        public PatientDataBase()
        {
            // TODO: implement
        }

        ~PatientDataBase()
        {
            // TODO: implement
        }

        public List<Patient> patient;
        public List<Allergen> allergens;

        public List<Allergen> getAllergens()
        {
            if (allergens == null)
                allergens = new List<Allergen>();
            return allergens;
        }

        public void addAllergen(Allergen newAllergen)
        {
            if (newAllergen == null)
                return;
            if (this.allergens == null)
                this.allergens = new List<Allergen>();
            if (!this.allergens.Contains(newAllergen))
                this.allergens.Add(newAllergen);
        }

        /// <pdGenerated>default getter</pdGenerated>
        public List<Patient> getPatient()
        {
            if (patient == null)
                patient = new List<Patient>();
            return patient;
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
            if (this.patient == null)
                this.patient = new List<Patient>();
            if (!this.patient.Contains(newPatient))
                this.patient.Add(newPatient);


        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemovePatient(Patient oldPatient)
        {
            if (oldPatient == null)
                return;
            if (this.patient != null)
                if (this.patient.Contains(oldPatient))
                    this.patient.Remove(oldPatient);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllPatient()
        {
            if (patient != null)
                patient.Clear();
        }

    }
}