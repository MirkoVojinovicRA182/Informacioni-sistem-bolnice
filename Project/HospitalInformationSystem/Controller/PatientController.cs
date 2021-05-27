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
        //private DoctorService doctorService;
        //private RoomService roomService;

        public static PatientController getInstance()
        {
            if (instance == null)
                instance = new PatientController();
            return instance;
        }
        private PatientController()
        {
            patientService = new PatientService();
            //doctorService = new DoctorService();
            //roomService = new RoomService();
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
            AccountController.GetInstance().AddNewAccount(new Account(username, "pass", new Patient(username, name, surname,
              dateOfBirth, phoneNumber, email, parentsName,
              gender, jmbg, isGuest, blood, lbo)));
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

        public DateTime NextValidTime(ref DateTime time)
        {
            return patientService.NextValidTime(ref time);
        }

        public void AddAppointment(ObservableCollection<Appointment> appointmentsList, ref DateTime time, Doctor doctor, Patient patient)
        {
            patientService.AddAppointment(appointmentsList, ref time, doctor, patient, RoomController.GetInstance().GetRooms(), DoctorController.getInstance().GetDoctors());
        }

        public bool IsDoctorFreeInTime(Doctor doctor, DateTime time)
        {
            return patientService.IsDoctorFreeInTime(doctor, time);
        }

        public bool CheckDoctorsAppointmentsInRoom(Room room, DateTime time)
        {
            return patientService.IsRoomFree(room, time, DoctorController.getInstance().GetDoctors());
        }

        public void FindNearestAppointments(ObservableCollection<Appointment> appointmentsList, Specialization specialization, Patient patient)
        {
            patientService.FindNearestAppointments(appointmentsList, specialization, patient, DoctorController.getInstance().GetDoctors(), RoomController.GetInstance().GetRooms());
        }
    }
}
