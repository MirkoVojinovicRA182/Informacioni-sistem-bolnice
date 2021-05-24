using HospitalInformationSystem.Model;
using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Patient;

namespace HospitalInformationSystem.Controller
{
    class PatientController
    {
        private static PatientController instance = null;
        private PatientService patientService;

        public static PatientController getInstance()
        {
            if (instance == null)
                instance = new PatientController();
            return instance;
        }
        private PatientController()
        {
            patientService = new PatientService();
        }

        public void SaveInFile()
        {
            patientService.SaveInFile();
        }

        public void LoadFromFile()
        {
            patientService.LoadFromFile();
        }

        public void CreatePatient(string username, string name, string surname,
            DateTime dateOfBirth, string phoneNumber, string email, string parentsName,
            string gender, string jmbg, bool isGuest, BloodType blood, string lbo)
        {
            patientService.CreatePatient(username,  name,  surname,
              dateOfBirth,  phoneNumber,  email,  parentsName,
              gender,  jmbg, isGuest,  blood,  lbo);
        }
        public ObservableCollection<Allergen> getAllergens()
        {
            return patientService.getAllergens();
        }

        public void setAllergens(ObservableCollection<Allergen> allergenList)
        {
            patientService.setAllergens(allergenList);
        }

        public void addAllergen(Allergen newAllergen)
        {
            patientService.addAllergen(newAllergen);
        }

        public ObservableCollection<Patient> getPatient()
        {
            return patientService.getPatient();
        }

        public void setPatient(ObservableCollection<Patient> newPatient)
        {
            patientService.setPatient(newPatient);
        }
        public void AddPatient(Patient newPatient)
        {
            patientService.AddPatient(newPatient);
        }

        public void RemovePatient(Patient oldPatient)
        {
            patientService.RemovePatient(oldPatient);
        }

        public void RemoveAllPatient()
        {
            patientService.RemoveAllPatient();
        }

        public void AddPrescription(Patient patient, Prescription prescription)
        {
            patientService.AddPrescription(patient, prescription);
        }

        public void AddHospitalTreatment(Patient patientToSendOnHospitalTreatmant, HospitalTreatment hospitalTreatment)
        {
            patientService.AddHospitalTreatment(patientToSendOnHospitalTreatmant, hospitalTreatment);
        }

        public void EditHospitalTreatment(Patient patientToEditHospitalTreatment, DateTime startTime, DateTime endTime, Room roomForTreatment)
        {
            patientService.EditHospitalTreatment(patientToEditHospitalTreatment, startTime, endTime, roomForTreatment);
        }

        public void AddAllergenToPatient(Patient patientToAddAllergen, string allergen)
        {
            patientService.AddAllergenToPatient(patientToAddAllergen, allergen);
        }

        public List<Patient> GetPatientsOnHospitalTretment()
        {
            return patientService.GetPatientsOnHospitalTretment();
        }
        public void saveInFile()
        {
            patientService.SaveInFile();
        }

        public void loadFromFile()
        {
            patientService.LoadFromFile();
        }
    }
}
