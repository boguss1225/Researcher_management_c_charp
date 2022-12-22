using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Model
{
    //define student class which inherits researcher properties
    public class Student:Researcher
    {
        public string Degree { get; set; }
        public int SupervisorID { get; set; }
        public string SupervisorName { get { return ERDAdapter.GetName(SupervisorID); } }
    }


}
