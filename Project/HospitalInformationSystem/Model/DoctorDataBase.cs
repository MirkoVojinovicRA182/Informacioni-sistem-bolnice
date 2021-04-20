/***********************************************************************
 * Module:  DoctorDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.DoctorDataBase
 ***********************************************************************/

using System.Collections.Generic;

namespace Model
{
    public class DoctorDataBase
    {

        public static DoctorDataBase instance;

        private List<Doctor> doctors;

        public static DoctorDataBase getInstance()
        {
            if(instance == null)
                instance = new DoctorDataBase();
            return instance;
        }
        private DoctorDataBase()
        {
            // TODO: implement
        }

        ~DoctorDataBase()
        {
            // TODO: implement
        }

        /*public string GetName(Doctor doctor)
        {
            // TODO: implement
            return "";
        }*/

        //public System.Collections.ArrayList doctor;

        /// <pdGenerated>default getter</pdGenerated>
        /*public System.Collections.ArrayList GetDoctor()
        {
            if (doctor == null)
                doctor = new System.Collections.ArrayList();
            return doctor;
        }*/
        public List<Doctor> GetDoctors()
        {
            if (doctors == null)
                doctors = new List<Doctor>();
            return doctors;
        }

        /// <pdGenerated>default setter</pdGenerated>
        /*public void SetDoctor(System.Collections.ArrayList newDoctor)
        {
            RemoveAllDoctor();
            foreach (Doctor oDoctor in newDoctor)
                AddDoctor(oDoctor);
        }*/
        public void SetDoctors(List<Doctor> newDoctor)
        {
            RemoveAllDoctors();
            foreach (Doctor oDoctor in newDoctor)
                AddDoctor(oDoctor);
        }

        /// <pdGenerated>default Add</pdGenerated>
        /*public void AddDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (this.doctor == null)
                this.doctor = new System.Collections.ArrayList();
            if (!this.doctor.Contains(newDoctor))
                this.doctor.Add(newDoctor);
        }*/
        public void AddDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (this.doctors == null)
                this.doctors = new List<Doctor>();
            if (!this.doctors.Contains(newDoctor))
                this.doctors.Add(newDoctor);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        /*public void RemoveDoctor(Doctor oldDoctor)
        {
            if (oldDoctor == null)
                return;
            if (this.doctor != null)
                if (this.doctor.Contains(oldDoctor))
                    this.doctor.Remove(oldDoctor);
        }*/
        public void RemoveDoctor(Doctor oldDoctor)
        {
            if (oldDoctor == null)
                return;
            if (this.doctors != null)
                if (this.doctors.Contains(oldDoctor))
                    this.doctors.Remove(oldDoctor);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllDoctors()
        {
            if (doctors != null)
                doctors.Clear();
        }

    }
}