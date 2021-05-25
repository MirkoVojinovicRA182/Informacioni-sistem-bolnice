/***********************************************************************
 * Module:  DoctorFileManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Repository.DoctorFileManipulation
 ***********************************************************************/

using Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HospitalInformationSystem.Repository
{
    public class DoctorRepository : IRepository
    {
        private List<Doctor> _allDoctorsInSystem;
        public DoctorRepository()
        {
            _allDoctorsInSystem = new List<Doctor>();
        }
        public void saveInFile()
        {
            FileStream fs = new FileStream("Doctors.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, _allDoctorsInSystem);
            }
            catch (SerializationException e)
            {
                throw;
            }
            finally
            {
                fs.Close();
            }

        }
        public void loadFromFile()
        {
            if (File.Exists("Doctors.dat"))
            {
                FileStream fs = new FileStream("Doctors.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    SetAllDoctors((List<Doctor>)formatter.Deserialize(fs));
                }
                catch (SerializationException e)
                {
                    throw;
                }
                finally
                {
                    fs.Close();
                }

            }
        }
        public List<Doctor> GetAllDoctors()
        {
            if (_allDoctorsInSystem == null)
                _allDoctorsInSystem = new List<Doctor>();
            return _allDoctorsInSystem;
        }
        public void SetAllDoctors(List<Doctor> allDoctors)
        {
            _allDoctorsInSystem.Clear();
            foreach (Doctor doctor in allDoctors)
                _allDoctorsInSystem.Add(doctor);
        }
        public void DeleteAllDoctors()
        {
            if (_allDoctorsInSystem != null)
                _allDoctorsInSystem.Clear();
        }
        public void DeleteDoctor(Doctor oldDoctor)
        {
            if (oldDoctor == null)
                return;
            if (_allDoctorsInSystem.Contains(oldDoctor))
                _allDoctorsInSystem.Remove(oldDoctor);
        }
    }
}