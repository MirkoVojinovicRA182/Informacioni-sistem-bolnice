/***********************************************************************
 * Module:  DoctorAppointmentsFIleManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Repository.DoctorAppointmentsFIleManipulation
 ***********************************************************************/

using HospitalInformationSystem.Controller;
using Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HospitalInformationSystem.Repository
{
    public class AppointmentsRepository : IRepository
    {
        public void saveInFile()
        {
            // TODO: implement
            FileStream fs = new FileStream("Appointments.dat", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, AppointmentController.getInstance().getAppointment());
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
            if (File.Exists("Appointments.dat"))
            {
                FileStream fs = new FileStream("Appointments.dat", FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    AppointmentController.getInstance().setAppointment((List<Appointment>)formatter.Deserialize(fs));
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