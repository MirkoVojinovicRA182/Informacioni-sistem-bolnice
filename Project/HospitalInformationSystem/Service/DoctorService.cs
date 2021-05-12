/***********************************************************************
 * Module:  DoctorDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.DoctorDataBase
 ***********************************************************************/

using Model;
using HospitalInformationSystem.Repository;
using System.Collections.Generic;

namespace HospitalInformationSystem.Service
{
    public class DoctorService
    {

        private List<Doctor> doctors;
        DoctorRepository doctorFile;

        public DoctorService()
        {
            doctors = new List<Doctor>();
            doctorFile = new DoctorRepository();
        }

        ~DoctorService() { }

        public List<Doctor> getDoctors()
        {
            if (doctors == null)
                doctors = new List<Doctor>();
            return doctors;
        }


        public void setDoctors(List<Doctor> newDoctor)
        {
            removeAllDoctors();
            foreach (Doctor oDoctor in newDoctor)
                addDoctor(oDoctor);
        }

        public void addDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (this.doctors == null)
                this.doctors = new List<Doctor>();
            if (!this.doctors.Contains(newDoctor))
                this.doctors.Add(newDoctor);
        }

        public void removeDoctor(Doctor oldDoctor)
        {
            if (oldDoctor == null)
                return;
            if (this.doctors != null)
                if (this.doctors.Contains(oldDoctor))
                    this.doctors.Remove(oldDoctor);
        }

        public void removeAllDoctors()
        {
            if (doctors != null)
                doctors.Clear();
        }

        public void saveInFile()
        {
            doctorFile.SaveInFile();
        }

        public void loadFromFile()
        {
            doctorFile.LoadFromFile();
        }

    }
}