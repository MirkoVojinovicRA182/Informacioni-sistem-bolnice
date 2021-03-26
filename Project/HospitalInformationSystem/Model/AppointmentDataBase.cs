/***********************************************************************
 * Module:  AppointmentDataBase.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.AppointmentDataBase
 ***********************************************************************/

using System;

namespace Model
{
   public class AppointmentDataBase
   {
      public AppointmentDataBase()
      {
         // TODO: implement
      }
      
      ~AppointmentDataBase()
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
            this.appointment.Add(newAppointment);
      }
      
      /// <pdGenerated>default Remove</pdGenerated>
      public void RemoveAppointment(Appointment oldAppointment)
      {
         if (oldAppointment == null)
            return;
         if (this.appointment != null)
            if (this.appointment.Contains(oldAppointment))
               this.appointment.Remove(oldAppointment);
      }
      
      /// <pdGenerated>default removeAll</pdGenerated>
      public void RemoveAllAppointment()
      {
         if (appointment != null)
            appointment.Clear();
      }
   
   }
}