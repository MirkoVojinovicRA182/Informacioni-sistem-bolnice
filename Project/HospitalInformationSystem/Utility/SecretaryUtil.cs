using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalInformationSystem.Service
{
    class SecretaryUtil
    {
        static public DateTime NextValidTime(ref DateTime time)
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

        static public void AddAppointment(ObservableCollection<Appointment> appointmentsList, ref DateTime time, Doctor doctor, Patient patient, List<Room> roomsList, ObservableCollection<Doctor> doctorsList)
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


        static public bool IsDoctorFreeInTime(Doctor doctor, DateTime time)
        {
            foreach (Appointment appointment in doctor.GetAppointment())
            {
                if (appointment.StartTime == time)
                    return false;
            }
            return true;
        }

        static public Room FindFreeRoom(DateTime time, List<Room> roomsList, ObservableCollection<Doctor> doctorsList)
        {
            foreach (Room room in roomsList)
            {
                if (room.Type != TypeOfRoom.OperationRoom)
                    continue;
                if (IsRoomFree(room, time))
                    return room;
            }
            return null;
        }

        static public bool IsRoomFree(Room room, DateTime time)
        {
            foreach (Doctor doctor in DoctorController.getInstance().GetDoctors())
            {
                foreach (Appointment appointment in doctor.GetAppointment())
                {
                    if (appointment.StartTime == time && appointment.room.Id == room.Id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void FindNearestAppointments(ObservableCollection<Appointment> appointmentsList, Specialization specialization, Patient patient)
        {
            DateTime timeToFind = DateTime.Now;

            for (int halfHour = 0; halfHour < 6; halfHour++)
            {
                NextValidTime(ref timeToFind);
                foreach (Doctor doctor in DoctorController.getInstance().GetDoctors())
                {
                    if (doctor.Specialization != specialization)
                        continue;

                    if (IsDoctorFreeInTime(doctor, timeToFind))
                    {
                        AddAppointment(appointmentsList, ref timeToFind, doctor, patient, RoomController.GetInstance().GetRooms(), DoctorController.getInstance().GetDoctors());
                        return;
                    }
                }
            }
        }

    }
}
