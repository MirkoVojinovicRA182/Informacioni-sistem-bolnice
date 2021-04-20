/***********************************************************************
 * Module:  Person.cs
 * Author:  Mirko
 * Purpose: Definition of the Class Model.Person
 ***********************************************************************/

using System;

namespace Model
{
    [Serializable]
    public class Person
    {
        public string Username
        { get; set; }

        public string Name
        { get; set; }

        public string Surname
        { get; set; }

        public DateTime DateOfBirth
        { get; set; }

        public string PhoneNumber
        { get; set; }

        public string Email
        { get; set; }

        public string ParentsName
        { get; set; }
        
        public string Gender
        { get; set; }

        public string Jmbg
        { get; set; }

        public Person()
        {
            // TODO: implement
        }

        ~Person()
        {
            // TODO: implement
        }

    }
}