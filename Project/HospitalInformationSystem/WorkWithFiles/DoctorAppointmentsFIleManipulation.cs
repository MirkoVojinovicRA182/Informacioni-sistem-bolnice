/***********************************************************************
 * Module:  DoctorAppointmentsFIleManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Class WorkWithFiles.DoctorAppointmentsFIleManipulation
 ***********************************************************************/

using Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WorkWithFiles
{
    public class DoctorAppointmentsFIleManipulation : IFileManipulation
    {
        public bool SaveInFile()
        {
            // TODO: implement
            FileStream fs = new FileStream("DoctorAppointments.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, AppointmentDataBase.getInstance().GetAppointment());
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
            if (File.Exists("DoctorAppointments.dat"))
            {
                FileStream fs = new FileStream("DoctorAppointments.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    AppointmentDataBase.getInstance().SetAppointment((List<Appointment>)formatter.Deserialize(fs));
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

            return true;
        }

    }
}