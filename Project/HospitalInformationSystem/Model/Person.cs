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
        public Person()
        {
            // TODO: implement
        }

        ~Person()
        {
            // TODO: implement
        }

        private string Country;
        private string City;

        public string Name
        {
            get;

            set;
           
        }

        public string Surname
        {
            get; set;
        }

        public string Id
        {
            get
            {
                // TODO: implement
                return (string)"";
            }
            set
            {
                // TODO: implement
            }
        }

    }
}