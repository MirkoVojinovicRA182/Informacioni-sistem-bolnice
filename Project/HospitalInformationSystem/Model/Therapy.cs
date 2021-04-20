using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Therapy
    {
        public Therapy(Medication medication, int dosage, List<DayOfWeek> days, DateTime time, bool notification)
        {
            this.Medication = medication;
            this.Dosage = dosage;
            this.Days = days;
            this.Time = time;
            this.NotificationEnabled = notification;
        }

        public int Dosage
        {
            get; set;
        }
        public bool NotificationEnabled
        {
            get; set;
        }
        public string NotificationEnabledString
        {
            get
            {
                if (NotificationEnabled)
                    return "Uključene";
                else
                    return "Isključene";
            } 
            set
            {

            }
        }
        public Medication Medication
        {
            get; set;
        }

        public DateTime Time
        {
            get; set;
        }

        public List<DayOfWeek> Days
        {
            get; set;
        }

        public string DaysString
        {
            get
            {
                string[] s = new string[7];
                for (int i = 0; i < Days.Count; i++)
                {
                    s[i] = Days[i].ToString();
                    s[i] = s[i].Replace("Monday", "Ponedeljak, ");
                    s[i] = s[i].Replace("Tuesday", "Utorak, ");
                    s[i] = s[i].Replace("Wednesday", "Sreda, ");
                    s[i] = s[i].Replace("Thursday", "Četvrtak, ");
                    s[i] = s[i].Replace("Friday", "Petak, ");
                    s[i] = s[i].Replace("Saturday", "Subota, ");
                    s[i] = s[i].Replace("Sunday", "Nedelja, ");
                }
                s[Days.Count - 1].Trim();
                string s1 = s[Days.Count-1].Replace(",", "");
                s[Days.Count-1] = s1;
                s1 = string.Join("", s);
                return s1;
            }
            set
            {

            }
        }

        public string TimeString
        {
            get
            {
                string s = "";
                DateTime time = Time;
                s = time.ToString("HH:mm");
                return s;
            }
            set
            {

            }
        }

    }

}
