/***********************************************************************
 * Module:  DoctorFileManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Repository.DoctorFileManipulation
 ***********************************************************************/

using HospitalInformationSystem.Controller;
using Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HospitalInformationSystem.Repository
{
    public class DoctorRepository : IRepository
    {
        public void SaveInFile()
        {
            // TODO: implement
            FileStream fs = new FileStream("Doctors.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, DoctorController.getInstance().getDoctors());
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
        public void LoadFromFile()
        {
            // TODO: implement
            if (File.Exists("Doctors.dat"))
            {
                FileStream fs = new FileStream("Doctors.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    DoctorController.getInstance().setDoctors((List<Doctor>)formatter.Deserialize(fs));
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

    }
}