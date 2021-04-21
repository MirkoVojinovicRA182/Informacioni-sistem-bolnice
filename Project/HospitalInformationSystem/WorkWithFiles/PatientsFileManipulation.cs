/***********************************************************************
 * Module:  PatientsFileManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Class WorkWithFiles.PatientsFileManipulation
 ***********************************************************************/

using Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WorkWithFiles
{
    public class PatientsFileManipulation : IFileManipulation
    {
        public bool SaveInFile()
        {
            FileStream fs = new FileStream("Accounts.dat", FileMode.Create);
            FileStream fs2 = new FileStream("Allergens.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, PatientDataBase.getInstance().getPatient());
                formatter.Serialize(fs2, PatientDataBase.getInstance().getAllergens());
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

            return true;
        }

        public bool LoadFromFile()
        {

            if (File.Exists("Allergens.dat"))
            {
                FileStream fs2 = new FileStream("Allergens.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    PatientDataBase.getInstance().setAllergens((List<Allergen>)formatter.Deserialize(fs2));
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
                    PatientDataBase.getInstance().setPatient((List<Patient>)formatter.Deserialize(fs));
                    //PatientDataBase.getInstance().getPatient()[0].setMedicalRecord(new MedicalRecord(1));
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



            return false;
        }

    }
}