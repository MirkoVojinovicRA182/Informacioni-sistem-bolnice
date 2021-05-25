using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Account
    {
        public Account(string username, string password, Person person)
        {
            Username = username;
            Password = password;
            Person = person;
        }

        public string Username
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public Person Person
        {
            get; set;
        }


    }
}
