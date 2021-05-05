using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    public enum AnswersDoctorSurvey
    {
        Da,
        Ne,
        Nijedno
    }
    public class DoctorReview
    {
        public DoctorReview(List<AnswersDoctorSurvey> answers, int rating, Doctor doctor)
        {
            Answers = answers;
            Rating = rating;
            Doctor = doctor;
        }

        public List<AnswersDoctorSurvey> Answers
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
