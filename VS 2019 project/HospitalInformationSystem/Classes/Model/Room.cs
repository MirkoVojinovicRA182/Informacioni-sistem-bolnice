/***********************************************************************
 * Module:  Room.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System;

namespace Model
{
   public class Room
   {
      public Room()
      {
         // TODO: implement
      }
      
      ~Room()
      {
         // TODO: implement
      }
   
      public System.Collections.ArrayList equipment;
      
      /// <pdGenerated>default getter</pdGenerated>
      public System.Collections.ArrayList GetEquipment()
      {
         if (equipment == null)
            equipment = new System.Collections.ArrayList();
         return equipment;
      }
      
      /// <pdGenerated>default setter</pdGenerated>
      public void SetEquipment(System.Collections.ArrayList newEquipment)
      {
         RemoveAllEquipment();
         foreach (Equipment oEquipment in newEquipment)
            AddEquipment(oEquipment);
      }
      
      /// <pdGenerated>default Add</pdGenerated>
      public void AddEquipment(Equipment newEquipment)
      {
         if (newEquipment == null)
            return;
         if (this.equipment == null)
            this.equipment = new System.Collections.ArrayList();
         if (!this.equipment.Contains(newEquipment))
            this.equipment.Add(newEquipment);
      }
      
      /// <pdGenerated>default Remove</pdGenerated>
      public void RemoveEquipment(Equipment oldEquipment)
      {
         if (oldEquipment == null)
            return;
         if (this.equipment != null)
            if (this.equipment.Contains(oldEquipment))
               this.equipment.Remove(oldEquipment);
      }
      
      /// <pdGenerated>default removeAll</pdGenerated>
      public void RemoveAllEquipment()
      {
         if (equipment != null)
            equipment.Clear();
      }
   
      private int Floor
      {
         get
         {
            // TODO: implement
            return (int)0;
         }
         set
         {
            // TODO: implement
         }
      }
      
      private int Id
      {
         get
         {
            // TODO: implement
            return (int)0;
         }
         set
         {
            // TODO: implement
         }
      }
      
      private TypeOfRoom Type
      {
         get
         {
            // TODO: implement
            return (TypeOfRoom)null;
         }
         set
         {
            // TODO: implement
         }
      }
   
   }
}