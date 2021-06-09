using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    [Serializable]
    public class HospitalReview : IReview
    {
        public List<AnswersHospitalSurvey> Answers
        {
            get; set;
        }
        public int Rating
        {
            get; set;
        }
    }
}
