using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    [Serializable]
    public class DoctorReview : IReview, IDoctorInformation
    {
        public List<AnswersHospitalSurvey> Answers
        {
            get; set;
        }
        public int Rating
        {
            get; set;
        }
        public Doctor Doctor
        {
            get; set;
        }
    }
}
