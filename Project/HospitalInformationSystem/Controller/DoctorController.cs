using Model;
using HospitalInformationSystem.Service;
using System.Collections.Generic;

namespace HospitalInformationSystem.Controller
{
    class DoctorController
    {

        DoctorService doctorService;

        private static DoctorController instance = null;

        public static DoctorController getInstance()
        {
            if (instance == null)
                instance = new DoctorController();
            return instance;
        }

        private DoctorController()
        {
            doctorService = new DoctorService();
        }

        public List<Doctor> GetDoctors()
        {
            return doctorService.getDoctors();
        }

        public void SetDoctors(List<Doctor> newDoctorsList)
        {
            doctorService.SetDoctors(newDoctorsList);
        }

        public void AddDoctor(Doctor newDoctor)
        {
            doctorService.AddDoctor(newDoctor);
        }

        public void RemoveDoctor(Doctor oldDoctor)
        {
            doctorService.RemoveDoctor(oldDoctor);
        }

        public void RemoveAllDoctors()
        {
            doctorService.RemoveAllDoctors();
        }

        public void SaveInFlie()
        {
            doctorService.SaveInFile();
        }

        public void LoadFromFile()
        {
            doctorService.LoadFromFile();
        }
    }
}
