using Model;
using HospitalInformationSystem.Service;
using System.Collections.Generic;
using System.IO;

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

        public List<Doctor> getDoctors()
        {
            return doctorService.getDoctors();
        }

        public void setDoctors(List<Doctor> newDoctor)
        {
            doctorService.setDoctors(newDoctor);
        }

        public void addDoctor(Doctor newDoctor)
        {
            doctorService.addDoctor(newDoctor);
        }

        public void removeDoctor(Doctor oldDoctor)
        {
            doctorService.removeDoctor(oldDoctor);
        }

        public void removeAllDoctors()
        {
            doctorService.removeAllDoctors();
        }

        public void saveInFlie()
        {
            doctorService.saveInFile();
        }

        public void loadFromFile()
        {
            if (File.Exists("Doctors.dat"))
                doctorService.loadFromFile();
        }
    }
}
