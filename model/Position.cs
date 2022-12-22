using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum EmploymentLevel { All, Student, A, B, C, D, E, }
    public class Position
    {
        public int ID { get; set; }
        public EmploymentLevel level {get;set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate{get;set;}
        public string levelName { get; set; }

        public override string ToString()
        {
            return StartDate.ToShortDateString() + "\t" + EndDate.ToShortDateString() + "\t" + levelName;
        }
    }

   



}
