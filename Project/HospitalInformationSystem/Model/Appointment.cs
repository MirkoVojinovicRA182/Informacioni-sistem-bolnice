/***********************************************************************
 * Module:  Appointment.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Appointment
 ***********************************************************************/

namespace Model
{
    public class Appointment
    {
        public Appointment()
        {
            // TODO: implement
        }

        public Patient patient;

        /// <pdGenerated>default parent getter</pdGenerated>
        public Patient GetPatient()
        {
            return patient;
        }

        /// <pdGenerated>default parent setter</pdGenerated>
        /// <param>newPatient</param>
        public void SetPatient(Patient newPatient)
        {
            if (this.patient != newPatient)
            {
                if (this.patient != null)
                {
                    Patient oldPatient = this.patient;
                    this.patient = null;
                    oldPatient.RemoveAppointment(this);
                }
                if (newPatient != null)
                {
                    this.patient = newPatient;
                    this.patient.AddAppointment(this);
                }
            }
        }
        public Doctor doctor;

        /// <pdGenerated>default parent getter</pdGenerated>
        public Doctor GetDoctor()
        {
            return doctor;
        }

        /// <pdGenerated>default parent setter</pdGenerated>
        /// <param>newDoctor</param>
        public void SetDoctor(Doctor newDoctor)
        {
            if (this.doctor != newDoctor)
            {
                if (this.doctor != null)
                {
                    Doctor oldDoctor = this.doctor;
                    this.doctor = null;
                    oldDoctor.RemoveAppointment(this);
                }
                if (newDoctor != null)
                {
                    this.doctor = newDoctor;
                    this.doctor.AddAppointment(this);
                }
            }
        }
        public Room room;

        private System.DateTime StartTime
        {
            get
            {
                // TODO: implement
                return System.DateTime.Now;
            }
            set
            {
                // TODO: implement
            }
        }

        private TypeOfAppointment Type
        {
            get
            {
                // TODO: implement
                return TypeOfAppointment.Pregled;
            }
            set
            {
                // TODO: implement
            }
        }

    }
}