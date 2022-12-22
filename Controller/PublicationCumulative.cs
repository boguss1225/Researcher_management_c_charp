using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Database;

namespace Controller
{
    public class PublicationCumulative
    {
        public int Count { get; set; }
        public int Year { get; set; }

      
        public override string ToString()
        {
            return "Year: " + Year + "  Count:   " + Count;
        }

    }
}
