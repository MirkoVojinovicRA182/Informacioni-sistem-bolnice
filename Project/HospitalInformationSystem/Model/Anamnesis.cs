using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    [Serializable]
    public class Anamnesis
    {

        public Anamnesis(DateTime date, String basicDescription, String anamnesis)
        {
            this.date = date;
            this.basicDescription = basicDescription;
            this.anamnesis = anamnesis;
        }

        public DateTime date
        {
            get;

            set;

        }

        public string basicDescription
        {
            get;

            set;

        }

        public string anamnesis
        {
            get;

            set;

        }


    }
}
