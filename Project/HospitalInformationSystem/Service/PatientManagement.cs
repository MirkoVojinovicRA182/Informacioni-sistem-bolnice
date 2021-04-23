/***********************************************************************
 * Module:  PatientManagement.cs
 * Author:  Mirko
 * Purpose: Definition of the Class BusinessLogic.PatientManagement
 ***********************************************************************/

using Model;
using System;
using static Model.Patient;

namespace BusinessLogic
{
    public class PatientManagement
    {
        public static bool CreatePatient(string username, string name, string surname,
            DateTime dateOfBirth, string phoneNumber, string email, string parentsName,
            string gender, string jmbg, bool isGuest, BloodType blood, string lbo)
        {
            Patient patient = new Patient(username, name, surname,
            dateOfBirth, phoneNumber, email, parentsName,
            gender, jmbg, isGuest, blood, lbo);
            System.Console.WriteLine("NAPRAVLJEN PACIJENT :" + patient);
            PatientDataBase.getInstance().AddPatient(patient);
            return true;
        }

        public bool DeletePatient(Patient patient)
        {
            // TODO: implement
            return false;
        }

        public int DeleteAllPatients()
        {
            // TODO: implement
            return 0;
        }

        public int ChangePatient(Patient patient)
        {
            // TODO: implement
            return 0;
        }

        public int ReadPatients()
        {
            // TODO: implement
            return 0;
        }

        internal PatientManagement()
        {
            // TODO: implement
        }

    }
}