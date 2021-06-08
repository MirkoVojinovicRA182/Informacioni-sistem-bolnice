using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public class DoctorRatingPair
    {
        public DoctorRatingPair(int rating, int number)
        {
            Rating = rating;
            NumberOfRatings = number;
        }
        public int Rating
        {
            get; set;
        }
        public int NumberOfRatings
        {
            get; set;
        }
    }
}
