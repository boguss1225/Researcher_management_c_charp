using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using System.Drawing;

namespace Model
{


    public class Researcher
    {
        public int ID { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Title { get; set; }
        public string photo { get; set; }
        public string Campus { get; set; }
        public EmploymentLevel Level { get; set; }
        public string Unit { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CurrentDate { get; set; }
        public List<Publication> Publi { get; set; }
        public List<Position> Position { get; set; }
        public Student stu { get; set; }
        public Staff sta { get; set; }


     

        public List<PublicationCumulative> Pulist { get; set; }

        //Return researcher's name according to requirements:
             public override string ToString()
        {
            return FamilyName + ", " + GivenName + " (" + Title + ")";
        }



        //Using switch & case when statement to get currentJobTitle from level
        public string GetCurrentJobTitle
        {
            get
            {
                string title = "";

                switch (Level)
                {
                    case (EmploymentLevel.Student):
                        title = "Student";
                        break;
                    case (EmploymentLevel.A):
                        title = "Postdoc";
                        break;
                    case (EmploymentLevel.B):
                        title = "Lecturer";
                        break;
                    case (EmploymentLevel.C):
                        title = "Senior Lecturer";
                        break;
                    case (EmploymentLevel.D):
                        title = "Associate Professor";
                        break;
                    case (EmploymentLevel.E):
                        title = "Professor";
                        break;
                }
                return title;
            }
        }

      
    

        // publication count 
        public int PublicationCount
        {
            get { return Publi == null ? 0 : Publi.Count(); }
        }

        // Tenure
        public double Tenure
        {
            get { return ((double)((DateTime.Today - StartDate).Days)) / 365; }
        }

        //Combined GiveName and FamilyName
        public string Name
        {
            get
            {
                return GivenName + " " + FamilyName;
            }
        }
          
    

        // calc 3yrAverage
        public double ThreeYearAverage
        {
            get
            {
                double count = 0.0;
                foreach (Publication e in Publi)
                {
                    if ((e.Age < 4) && (e.Age >= 1))
                    {
                        count = count + 1.0;
                    }
                }
                return Math.Round((double)(count / 3), 1);
            }
        }

        //Using switch to calc performance by 3yrAverage for each level
        public string performance
        {

            get
            {
                string performance = "";
                switch (Level)
                {
                    case EmploymentLevel.Student:
                        performance = "None";
                        break;
                    case EmploymentLevel.A:
                        performance = ((ThreeYearAverage) / 0.5 * 100).ToString("f") + "%";
                        break;
                    case EmploymentLevel.B:
                        performance = ((ThreeYearAverage) / 1.0 * 100).ToString("f") + "%";
                        break;
                    case EmploymentLevel.C:
                        performance = ((ThreeYearAverage) / 2.0 * 100).ToString("f") + "%";
                        break;
                    case EmploymentLevel.D:
                        performance = ((ThreeYearAverage) / 3.2 * 100).ToString("f") + "%";
                        break;
                    case EmploymentLevel.E:
                        performance = ((ThreeYearAverage) / 4.0 * 100).ToString("f") + "%";
                        break;
                }
                return performance;
            }

        }

        public string performanceS
        {

            get
            {
                string performanceS = "none";
                double performance=0;
                switch (Level)
                {
                    case EmploymentLevel.Student:
                        performance = -1;
                        break;
                    case EmploymentLevel.A:
                        performance = ((ThreeYearAverage) / 0.5 * 100) ;
                        break;
                    case EmploymentLevel.B:
                        performance = ((ThreeYearAverage) / 1.0 * 100);
                        break;
                    case EmploymentLevel.C:
                        performance = ((ThreeYearAverage) / 2.0 * 100);
                        break;
                    case EmploymentLevel.D:
                        performance = ((ThreeYearAverage) / 3.2 * 100);
                        break;
                    case EmploymentLevel.E:
                        performance = ((ThreeYearAverage) / 4.0 * 100);
                        break;
                }
                if (performance <= 70 && performance >= 0)
                {
                    performanceS = "Poor";
                }
                else if (performance > 70 && performance < 110)
                {
                    performanceS = "Below Expectations";
                }
                else if (performance >= 110 && performance < 200)
                {
                    performanceS = "Meeting Minimum";
                }
                else if(performance >= 200)
                {
                    performanceS = "Star Performers";
                }
                return performanceS;
            }

        }
    }
}