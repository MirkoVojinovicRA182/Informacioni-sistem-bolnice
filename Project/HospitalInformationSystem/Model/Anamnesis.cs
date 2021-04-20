using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{

    [Serializable]
    class Anamnesis
    {
        private DateTime date;
        private String basicDescription;
        private String anamnesis;

        public Anamnesis(DateTime date, String basicDescription, String anamnesis)
        {
            this.date = date;
            this.basicDescription = basicDescription;
            this.anamnesis = anamnesis;
        }

        public DateTime getDate()
        {
            return date;
        }

        public String getBasicDescription()
        {
            return basicDescription;
        }

        public String getAnamnesis()
        {
            return anamnesis;
        }

    }
}
