/***********************************************************************
 * Module:  DoctorDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.DoctorDataBase
 ***********************************************************************/

using Model;
using HospitalInformationSystem.Repository;
using System.Collections.Generic;
using System.IO;

namespace HospitalInformationSystem.Service
{

    public class DoctorService
    {

        private List<Doctor> doctorsList;
        private DoctorRepository doctorsFile;

        public DoctorService()
        {
            doctorsList = new List<Doctor>();
            doctorsFile = new DoctorRepository();
        }

        ~DoctorService()
        {
        }

        public List<Doctor> getDoctors()
        {
            if (doctorsList == null)
                doctorsList = new List<Doctor>();
            return doctorsList;
        }


        public void SetDoctors(List<Doctor> newDoctorsList)
        {
            RemoveAllDoctors();
            foreach (Doctor oDoctor in newDoctorsList)
                AddDoctor(oDoctor);
        }

        public void AddDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (this.doctorsList == null)
                this.doctorsList = new List<Doctor>();
            if (!this.doctorsList.Contains(newDoctor))
                this.doctorsList.Add(newDoctor);
        }

        public void RemoveDoctor(Doctor oldDoctor)
        {
            if (oldDoctor == null)
                return;
            if (this.doctorsList != null)
                if (this.doctorsList.Contains(oldDoctor))
                    this.doctorsList.Remove(oldDoctor);
        }

        public void RemoveAllDoctors()
        {
            if (doctorsList != null)
                doctorsList.Clear();
        }

        public void SaveInFile()
        {
            doctorsFile.saveInFile();
        }

        public void LoadFromFile()
        {
            doctorsFile.loadFromFile();
        }

    }
}