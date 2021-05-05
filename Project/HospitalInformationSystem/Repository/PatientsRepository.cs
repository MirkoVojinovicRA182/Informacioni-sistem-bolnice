/***********************************************************************
 * Module:  PatientsFileManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Repository.PatientsFileManipulation
 ***********************************************************************/

using HospitalInformationSystem.Controller;
using Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HospitalInformationSystem.Repository
{
    public class PatientsRepository : IRepository
    {
        public void saveInFile()
        {
            FileStream fs = new FileStream("Accounts.dat", FileMode.Create);
            FileStream fs2 = new FileStream("Allergens.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, PatientController.getInstance().getPatient());
                formatter.Serialize(fs2, PatientController.getInstance().getAllergens());
            }
            catch (SerializationException e)
            {

                throw;
            }
            finally
            {
                fs.Close();
                fs2.Close();
            }

        }

        public void loadFromFile()
        {

            if (File.Exists("Allergens.dat"))
            {
                FileStream fs2 = new FileStream("Allergens.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    PatientController.getInstance().setAllergens((List<Allergen>)formatter.Deserialize(fs2));
                }
                catch (SerializationException e)
                {
                    throw;
                }
                finally
                {
                    fs2.Close();
                }

            }

            if (File.Exists("Accounts.dat"))
            {
                FileStream fs = new FileStream("Accounts.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    PatientController.getInstance().setPatient((List<Patient>)formatter.Deserialize(fs));
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