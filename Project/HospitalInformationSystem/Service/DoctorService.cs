/***********************************************************************
 * Module:  DoctorDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.DoctorDataBase
 ***********************************************************************/

using Model;
using HospitalInformationSystem.Repository;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace HospitalInformationSystem.Service
{

    public class DoctorService
    {

        private ObservableCollection<Doctor> doctorsList;
        private DoctorRepository doctorsFile;

        public DoctorService()
        {
            doctorsList = new ObservableCollection<Doctor>();
            doctorsFile = new DoctorRepository();
        }

        ~DoctorService()
        {
        }

        public ObservableCollection<Doctor> getDoctors()
        {
            if (doctorsList == null)
                doctorsList = new ObservableCollection<Doctor>();
            return doctorsList;
        }


        public void SetDoctors(ObservableCollection<Doctor> newDoctorsList)
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
                this.doctorsList = new ObservableCollection<Doctor>();
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