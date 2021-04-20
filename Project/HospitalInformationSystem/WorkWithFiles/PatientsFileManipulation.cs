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

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, PatientDataBase.getInstance().getPatient());
            }
            catch (SerializationException e)
            {

                throw;
            }
            finally
            {
                fs.Close();
            }

            return true;
        }

        public bool LoadFromFile()
        {
            if (File.Exists("Accounts.dat"))
            {
                FileStream fs = new FileStream("Accounts.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    PatientDataBase.getInstance().setPatient((List<Patient>)formatter.Deserialize(fs));
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