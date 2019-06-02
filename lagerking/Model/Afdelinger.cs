using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace lagerking
{
    public class Afdelinger : ObservableCollection<string>
    {
        public Afdelinger()
        {
            Add("A");
            Add("B");
            Add("C");
            Add("D");

        }
    }
}
