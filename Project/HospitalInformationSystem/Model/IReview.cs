using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public enum AnswersHospitalSurvey
{
    Veoma_zadovoljni,
    Zadovoljni,
    Nezadovoljni
}
namespace HospitalInformationSystem.Model
{
    public interface IReview
    {
        List<AnswersHospitalSurvey> Answers
        {
            get; set;
        }

        int Rating
        {
            get; set;
        }
    }
}
