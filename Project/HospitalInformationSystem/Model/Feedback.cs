using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    [Serializable]
    public class Feedback
    {
        public Feedback(string person, DateTime date, string content)
        {
            Person = person;
            Date = date;
            Content = content;
        }
        String Person { get; set; }
        DateTime Date { get; set; }
        String Content { get; set; }
    }
}
