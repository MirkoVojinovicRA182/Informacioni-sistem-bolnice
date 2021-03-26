/***********************************************************************
 * Module:  Doctor.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Doctor
 ***********************************************************************/

using System;

namespace Model
{
   public class Doctor : Person
   {
      public Doctor()
      {
         // TODO: implement
      }
   
      public System.Collections.ArrayList appointment;
      
      /// <pdGenerated>default getter</pdGenerated>
      public System.Collections.ArrayList GetAppointment()
      {
         if (appointment == null)
            appointment = new System.Collections.ArrayList();
         return appointment;
      }
      
      /// <pdGenerated>default setter</pdGenerated>
      public void SetAppointment(System.Collections.ArrayList newAppointment)
      {
         RemoveAllAppointment();
         foreach (Appointment oAppointment in newAppointment)
            AddAppointment(oAppointment);
      }
      
      /// <pdGenerated>default Add</pdGenerated>
      public void AddAppointment(Appointment newAppointment)
      {
         if (newAppointment == null)
            return;
         if (this.appointment == null)
            this.appointment = new System.Collections.ArrayList();
         if (!this.appointment.Contains(newAppointment))
         {
            this.appointment.Add(newAppointment);
            newAppointment.SetDoctor(this);      
         }
      }
      
      /// <pdGenerated>default Remove</pdGenerated>
      public void RemoveAppointment(Appointment oldAppointment)
      {
         if (oldAppointment == null)
            return;
         if (this.appointment != null)
            if (this.appointment.Contains(oldAppointment))
            {
               this.appointment.Remove(oldAppointment);
               oldAppointment.SetDoctor((Doctor)null);
            }
      }
      
      /// <pdGenerated>default removeAll</pdGenerated>
      public void RemoveAllAppointment()
      {
         if (appointment != null)
         {
            System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
            foreach (Appointment oldAppointment in appointment)
               tmpAppointment.Add(oldAppointment);
            appointment.Clear();
            foreach (Appointment oldAppointment in tmpAppointment)
               oldAppointment.SetDoctor((Doctor)null);
            tmpAppointment.Clear();
         }
      }
      public Room room;
   
      private Specialization Specialization;
   
   }
}