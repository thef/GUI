using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lagerking;

namespace lagerking
{
    [Serializable]
    public class ProduktIndex
    {
        string navn;
        int pris;
        int antal;
        string afdeling;
        string description;

        public ProduktIndex()
        {
        }


        public ProduktIndex(string aNavn, int aPris, int aAntal, string aAfdeling, string aDescription)
        {
            navn = aNavn;
            pris = aPris;
            antal = aAntal;
            afdeling = aAfdeling;
            description = aDescription;
            
        }

        public string NAVN { get; set; }

        public int PRIS { get; set; }

        public int ANTAL { get; set; }

        public string DESCRIPTION { get; set; }

        public string AFDELING { get; set; }




    }
}
