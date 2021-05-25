/***********************************************************************
 * Module:  DoctorDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.DoctorDataBase
 ***********************************************************************/

using Model;
using HospitalInformationSystem.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HospitalInformationSystem.Controller;

namespace HospitalInformationSystem.Service
{
    public class DoctorService
    {
        private DoctorRepository _doctorsFile;
        public DoctorService() => _doctorsFile = new DoctorRepository();
        ~DoctorService()
        {
        }
        public ObservableCollection<Doctor> getDoctors() => _doctorsFile.GetAllDoctors();
        public void SetDoctors(ObservableCollection<Doctor> newDoctorsList) => _doctorsFile.SetAllDoctors(newDoctorsList);
        public void AddDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (!_doctorsFile.GetAllDoctors().Contains(newDoctor))
                _doctorsFile.GetAllDoctors().Add(newDoctor);
            AccountController.GetInstance().AddNewAccount(new Account(newDoctor.Username, "pass", newDoctor));
        }
        public void RemoveDoctor(Doctor oldDoctor) => _doctorsFile.DeleteDoctor(oldDoctor);
        public void RemoveAllDoctors() => _doctorsFile.DeleteAllDoctors();
        public void SaveInFile() => _doctorsFile.saveInFile();
        public void LoadFromFile() => _doctorsFile.loadFromFile();
    }
}