/***********************************************************************
 * Module:  PatientsFileManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Repository.PatientsFileManipulation
 ***********************************************************************/

using HospitalInformationSystem.Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HospitalInformationSystem.Repository
{
    public class PatientsRepository : IRepository
    {
        public ObservableCollection<Patient> patients;

        public PatientsRepository()
        {
            patients = new ObservableCollection<Patient>();
        }

        public void saveInFile()
        {
            FileStream fs = new FileStream("Patients.dat", FileMode.Create);
            

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, patients);
            }
            catch (SerializationException e)
            {
                throw e;
            }
            finally
            {
                fs.Close();
            }

        }

        public void loadFromFile()
        {
            if (File.Exists("Patients.dat"))
            {
                FileStream fs = new FileStream("Patients.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    patients = (ObservableCollection<Patient>)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    throw e;
                }
                finally
                {
                    fs.Close();
                }
            }
        }
    }
}