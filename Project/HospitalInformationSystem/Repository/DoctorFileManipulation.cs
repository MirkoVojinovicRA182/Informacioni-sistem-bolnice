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

namespace Repository
{
    public class DoctorFileManipulation : IFileManipulation
    {
        public bool SaveInFile()
        {
            // TODO: implement
            FileStream fs = new FileStream("Doctors.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, DoctorDataBase.getInstance().GetDoctors());
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
            // TODO: implement
            if (File.Exists("Doctors.dat"))
            {
                FileStream fs = new FileStream("Doctors.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    DoctorDataBase.getInstance().SetDoctors((List<Doctor>)formatter.Deserialize(fs));
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