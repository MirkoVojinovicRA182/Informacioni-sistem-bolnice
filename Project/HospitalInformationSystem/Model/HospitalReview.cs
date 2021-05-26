using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public enum AnswersHospitalSurvey
    {
        [Description("Veoma zadovoljni")]
        Veoma_zadovoljni,
        Zadovoljni,
        Nezadovoljni
        
    }
    [Serializable]
    public class HospitalReview
    {
        public HospitalReview(List<AnswersHospitalSurvey> answers, int rating)
        {
            Answers = answers;
            Rating = rating;
        }

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
