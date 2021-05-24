using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public class Announcement
    {
        public string Text { get; set; }
        
        public Announcement(string text)
        {
            Text = text;
        }
    }
}
