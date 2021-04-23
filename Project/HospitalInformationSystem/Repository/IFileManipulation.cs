/***********************************************************************
 * Module:  IFileManipulation.cs
 * Author:  Mirko
 * Purpose: Definition of the Interface Repository.IFileManipulation
 ***********************************************************************/

namespace HospitalInformationSystem.Repository
{
    public interface IFileManipulation
    {
        void saveInFile();
        void loadFromFile();
    }
}