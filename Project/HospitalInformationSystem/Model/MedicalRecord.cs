using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    [Serializable]
    public class MedicalRecord
    {

        private int id;
        private List<Anamnesis> anamnesisList;
        private List<Prescription> prescriptionList;
        public ObservableCollection<Allergen> AllergensList { get; set; }

        public MedicalRecord()
        {
            AllergensList = new ObservableCollection<Allergen>();
            prescriptionList = new List<Prescription>();
            anamnesisList = new List<Anamnesis>();
            foreach (Allergen allergen in PatientController.getInstance().getAllergens())
            {
                AllergensList.Add(new Allergen(allergen));
            }  
        }

        public MedicalRecord(int id)
        {
            this.id = id;
            anamnesisList = new List<Anamnesis>();
            prescriptionList = new List<Prescription>();
            AllergensList = new ObservableCollection<Allergen>();
            foreach (Allergen allergen in PatientController.getInstance().getAllergens())
            {
                AllergensList.Add(new Allergen(allergen));
            }
        }

        public int getId()
        {
            return id;
        }

        public void addAnamnesis(Anamnesis anamnesis)
        {
            if (anamnesisList == null)
                anamnesisList = new List<Anamnesis>();
            anamnesisList.Add(anamnesis);
        }

        public List<Anamnesis> getAnamneses()
        {
            return anamnesisList;
        }

        public void addPrescription(Prescription prescription)
        {
            prescriptionList.Add(prescription);
        }

        public List<Prescription> getPrescriptions()
        {
            return prescriptionList;
        }
    }
}
