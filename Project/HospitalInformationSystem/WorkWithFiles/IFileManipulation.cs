/***********************************************************************
 * Module:  IFileManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Interface WorkWithFiles.IFileManipulation
 ***********************************************************************/

using System;

namespace WorkWithFiles
{
   public interface IFileManipulation
   {
      bool SaveInFile();
      bool LoadFromFile();
   }
}