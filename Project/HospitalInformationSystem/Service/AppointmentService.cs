/***********************************************************************
 * Module:  AppointmentDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.AppointmentDataBase
 ***********************************************************************/

using Model;
using HospitalInformationSystem.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Collections.ObjectModel;
using HospitalInformationSystem.Model;

namespace HospitalInformationSystem.Service
{
    public class AppointmentService
    {
        private AppointmentsRepository _repository;
        public AppointmentService()
        {
            _repository = new AppointmentsRepository();
        }
        public void AddAppointmentToAppointmentList(Appointment newAppointment)
        {
            _repository.AddAppointmentToAppointmentList(newAppointment);
        }
        public List<Appointment> GetAppointments()
        {
            return _repository.GetAppointments();
        }
        public void SetAppointments(List<Appointment> appointments)
        {
            _repository.SetAppointments(appointments);
        }
        public void DeleteAppointment(Appointment appointment)
        {
            _repository.DeleteAppointment(appointment);
        }
        public void ChangeHospitalResidence(IHospitalResidence oldHospitalResidence, IHospitalResidence newHospitalResidence)
        {
            oldHospitalResidence.ChangeResidence(newHospitalResidence);
        }
        public List<Appointment> FindAppointmentByRoom(Room room)
        {
            return _repository.FindAppointmentByRoom(room);
        }
        public List<Appointment> GetAppointmentsByPatient(Patient patient)
        {
            return _repository.GetAppointmentsByPatient(patient);
        }
        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            return _repository.GetAppointmentsByDoctor(doctor);
        }
        public void SaveAppointmentsInFile()
        {
            _repository.saveInFile();
        }
        public void LoadAppointmentsFromFile()
        {
            _repository.loadFromFile();
        }
        public void DeleteAllAppointmentsFromRoom(Room room)
        {
            foreach (Appointment appointmentForRemove in FindAppointmentByRoom(room))
            {
                DeleteAppointment(appointmentForRemove);
            }
        }
        public void RemoveAllAppointments()
        {
            if (GetAppointments() != null)
                GetAppointments().Clear();
        }

        public bool AppointmentIsTaken(Appointment appointment)
        {
            bool isTaken = false;
            foreach (var currentAppointment in _repository.GetAppointments())
            {
                if (currentAppointment.Doctor == appointment.Doctor && currentAppointment.StartTime == appointment.StartTime)
                    return true;
            }
            return isTaken;
        }
        public List<Appointment> GenerateFreeAppointments(Doctor doctor, Patient patient, DateTime startDateTime, DateTime endDateTime)
        {
            List<Appointment> existingAppointments = GetAppointments();

            List<DateTime> dateTimes = new List<DateTime>();
            List<string> timesString = new List<string>();
            timesString.AddRange(new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30" , "12:00", "12:30", "13:00", "13:30",
                                                    "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30" , "18:00", "18:30", "19:00", "19:30"
                                                  });

            List<DateTime> dates = new List<DateTime>();
            List<string> datesString = new List<string>();

            for (var date = startDateTime; date <= endDateTime; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            for (int i = 0; i < dates.Count; i++)
            {
                datesString.Add(dates[i].Date.ToString("dd.MM.yyyy"));
            }

            for (int i = 0; i < datesString.Count; i++)
            {
                for (int j = 0; j < timesString.Count; j++)
                {
                    string s = datesString[i] + "." + " " + timesString[j];
                    dateTimes.Add(DateTime.ParseExact(s, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture));
                }
            }
            List<Appointment> freeAppointments = GenerateAllPossibleAppointments(doctor, patient, dateTimes);
            RemoveExistingAppointments(existingAppointments, freeAppointments);

            return freeAppointments;
        }
        public List<Appointment> GenerateAllPossibleAppointments(Doctor doctor, Patient patient, List<DateTime> dateTimes)
        {
            List<Appointment> freeAppointments = new List<Appointment>();
            for (int i = 0; i < dateTimes.Count; i++)
            {
                freeAppointments.Add(new Appointment(dateTimes[i], TypeOfAppointment.Pregled, doctor.room, patient, doctor));
            }
            return freeAppointments;
        }
        public void RemoveExistingAppointments(List<Appointment> existingAppointments, List<Appointment> recommendedAppointments)
        {
            for (int i = 0; i < recommendedAppointments.Count; i++)
            {
                for (int j = 0; j < existingAppointments.Count; j++)
                {
                    if (existingAppointments[j].Doctor == recommendedAppointments[i].Doctor & existingAppointments[j].StartTime == recommendedAppointments[i].StartTime)
                    {
                        recommendedAppointments.RemoveAt(i);
                    }
                }
            }
        }
        public List<Appointment> GenerateAppointmentsForDoctorsOfSameSpecialization(Doctor selectedDoctor, Patient patient, DateTime startDateTime, DateTime endDateTime)
        {
            List<Appointment> possibleAppointments = new List<Appointment>();
            DoctorService service = new DoctorService();
            ObservableCollection<Doctor> possibleDoctors = service.GetDoctorsWithSameSpecialization(selectedDoctor);
            foreach (var doctor in possibleDoctors)
            {
                possibleAppointments.AddRange(GenerateFreeAppointments(doctor, patient, startDateTime, endDateTime));
            }
            return possibleAppointments;
        }
        public void CalculateFinishedAppointments(Patient patient)
        {
            foreach (var appointment in GetAppointmentsByPatient(patient))
            {
                if (AppointmentIsFinished(appointment) && appointment.StartTime.AddMinutes(30).CompareTo(patient.Activity.HospitalReviewTime) > 0)
                {
                    patient.Activity.NumberOfFinishedAppointmentsSinceReview++;
                }
            }
        }
        public bool AppointmentIsFinished(Appointment appointment)
        {
            return DateTime.Now.CompareTo(appointment.StartTime.AddMinutes(30)) > 0;
        }
    }
}