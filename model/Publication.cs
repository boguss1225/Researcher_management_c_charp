using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum Pubtype
    {
        Conference, Journal, Other
    }
    public class Publication
    {
        public string DOI { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Year { get; set; }
        public DateTime AvailableDate { get; set; }
        public Pubtype Pubtype { get; set; }
        public int PubCount { get; set; }
        public string Cite_as { get; set; }


      //Define age for caculating three year average 
        public double Age
        {
            get
            {
                return Math.Round((((DateTime.Today - AvailableDate).Days) / 365.0), 1) ;
            }
        }

      
        public override string ToString()
        {
            return "'"+ Title + "' in " + AvailableDate.Year;
        }
    }
}