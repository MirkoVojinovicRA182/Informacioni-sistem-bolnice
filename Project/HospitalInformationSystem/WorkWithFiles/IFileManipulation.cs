/***********************************************************************
 * Module:  IFileManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Interface WorkWithFiles.IFileManipulation
 ***********************************************************************/

namespace WorkWithFiles
{
    public interface IFileManipulation
    {
        bool SaveInFile();
        bool LoadFromFile();
    }
}