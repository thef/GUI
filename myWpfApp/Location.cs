using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWpfApp
{
    class Location
    {
        public int Id { get; set; }

        public string Navn { get; set; }

        public string Vej { get; set; }

        public int Vejnummer { get; set; }

        public int Postnummer { get; set; }

        public string By { get; set; }

        public  List<Tree> Trees { get; set; }
    }
}
