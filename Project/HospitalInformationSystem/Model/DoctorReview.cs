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
        Yes,
        No,
        Neither
    }
    public class DoctorReview
    {
        public DoctorReview(List<AnswersDoctorSurvey> answers, Doctor doctor)
        {
            Answers = answers;
            Doctor = doctor;
        }

        public List<AnswersDoctorSurvey> Answers
        {
            get; set;
        }
        public Doctor Doctor
        {
            get; set;
        }


    }

}
