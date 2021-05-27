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
using System.Windows;
using static Model.Patient;

namespace HospitalInformationSystem.Service
{
    public class PatientService
    {
        private PatientsRepository _patientsFile;

        public PatientService()
        {
            _patientsFile = new PatientsRepository();
        }

        public void CreatePatient(string username, string name, string surname,
            DateTime dateOfBirth, string phoneNumber, string email, string parentsName,
            string gender, string jmbg, bool isGuest, BloodType blood, string lbo)
        {
            Patient patient = new Patient(username, name, surname,
            dateOfBirth, phoneNumber, email, parentsName,
            gender, jmbg, isGuest, blood, lbo);
            _patientsFile.patients.Add(patient);
        }

        public ObservableCollection<Allergen> getAllergens()
        {
            if (_patientsFile.allergens == null)
                _patientsFile.allergens = new ObservableCollection<Allergen>();
            return _patientsFile.allergens;
        }

        public void setAllergens(ObservableCollection<Allergen> allergenList)
        {
            _patientsFile.allergens = allergenList;
        }

        public void addAllergen(Allergen newAllergen)
        {
            if (newAllergen == null)
                return;
            if (_patientsFile.allergens == null)
                _patientsFile.allergens = new ObservableCollection<Allergen>();

            foreach (Allergen allergen in _patientsFile.allergens)
            {
                if (allergen.Name == newAllergen.Name)
                    return;
            }
            _patientsFile.allergens.Add(newAllergen);
        }

        /// <pdGenerated>default getter</pdGenerated>
        public ObservableCollection<Patient> getPatient()
        {
            if (_patientsFile.patients == null)
                _patientsFile.patients = new ObservableCollection<Patient>();
            return _patientsFile.patients;
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
            if (_patientsFile.patients == null)
                _patientsFile.patients = new ObservableCollection<Patient>();
            if (!_patientsFile.patients.Contains(newPatient))
                _patientsFile.patients.Add(newPatient);


        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemovePatient(Patient oldPatient)
        {
            if (oldPatient == null)
                return;
            if (_patientsFile.patients != null)
                if (_patientsFile.patients.Contains(oldPatient))
                    _patientsFile.patients.Remove(oldPatient);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllPatient()
        {
            if (_patientsFile.patients != null)
                _patientsFile.patients.Clear();
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
            foreach(Patient patient in getPatient())
            {
                if (patient.hospitalTreatment != null) patientsOnHospitalTretment.Add(patient);
            }
            return patientsOnHospitalTretment;
        }
        public void SaveInFile()
        {
            _patientsFile.saveInFile();
        }

        public void LoadFromFile()
        {
            _patientsFile.loadFromFile();
        }

        public DateTime NextValidTime(ref DateTime time)
        {
            if (time.Minute < 30)
            {
                time = time.Date + new TimeSpan(time.Hour, 30, 0);
            }
            else
            {
                time = time.Date + new TimeSpan(time.Hour + 1, 0, 0);
            }

            return time;
        }

        public void AddAppointment(ObservableCollection<Appointment> appointmentsList, ref DateTime time, Doctor doctor, Patient patient, List<Room> roomsList, ObservableCollection<Doctor> doctorsList)
        {
            Room room = FindFreeRoom(time, roomsList, doctorsList);

            if (room == null)
            {
                MessageBox.Show("NEMA SLOBODNIH SOBA ZA OPERACIJU!",
                              "Obavestenje",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return;
            }
            appointmentsList.Add(new Appointment(time, TypeOfAppointment.Operacija, room, patient, doctor));
        }


        public bool IsDoctorFreeInTime(Doctor doctor, DateTime time)
        {
            foreach (Appointment appointment in doctor.GetAppointment())
            {
                if (appointment.StartTime == time)
                    return false;
            }
            return true;
        }

        public Room FindFreeRoom(DateTime time, List<Room> roomsList, ObservableCollection<Doctor> doctorsList)
        {
            foreach (Room room in roomsList)
            {
                if (room.Type != TypeOfRoom.OperationRoom)
                    continue;
                if (IsRoomFree(room, time, doctorsList))
                    return room;
            }
            return null;
        }

        public bool IsRoomFree(Room room, DateTime time, ObservableCollection<Doctor> doctorsList)
        {
            foreach (Doctor doctor in doctorsList)
            {
                foreach (Appointment appointment in doctor.GetAppointment())
                {
                    if (appointment.StartTime == time && appointment.room == room)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void FindNearestAppointments(ObservableCollection<Appointment> appointmentsList, Specialization specialization, Patient patient, ObservableCollection<Doctor> doctorsList, List<Room> roomsList)
        {
            DateTime timeToFind = DateTime.Now;

            for (int halfHour = 0; halfHour < 6; halfHour++)
            {
                NextValidTime(ref timeToFind);
                foreach (Doctor doctor in doctorsList)
                {
                    if (doctor.Specialization != specialization)
                        continue;

                    if (IsDoctorFreeInTime(doctor, timeToFind))
                    {
                        AddAppointment(appointmentsList, ref timeToFind, doctor, patient, roomsList, doctorsList);
                        return;
                    }
                }
            }
        }
    }
}