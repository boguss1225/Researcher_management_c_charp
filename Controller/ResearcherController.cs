using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Model;
using Database;

namespace Controller
{
    class ResearcherController
    {
        private List<Researcher> researcherList;
        public List<Researcher> Workers { get { return researcherList; } set { } }
        private ObservableCollection<Researcher> viewableResearcher;
        public ObservableCollection<Researcher> VisibleResearcher { get { return viewableResearcher; } set { } }
        
        public ResearcherController()
        {
            researcherList = ERDAdapter.LoadAll();//Read all of the researcher information which selected through SQL in database class
            viewableResearcher = new ObservableCollection<Researcher>(researcherList); 
            

            foreach (Researcher e in researcherList)
            {
                e.Position = ERDAdapter.LoadPosition(e.ID);
                e.Pulist = ERDAdapter.PublicationCumulative(e.ID);
                e.Publi = ERDAdapter.LoadPublication(e.ID);

                //e.show = false;
            
                if ( 
                    e.Level == EmploymentLevel.Student)
                {
                    e.stu = ERDAdapter.LoadStudent(e.ID);
                }
                else
                {
                    e.sta = ERDAdapter.LoadStaff(e.ID);
                                
                }
            }
        }

        //filter method for name 
        public void Filter(string name)
        {
            SearchByName(name);
       
          
        }

        //filter method for level 
        public void FilterLevel(string level)
        {
            SearchByLevel(ParseEnum<EmploymentLevel>(level));
      

        }

        //filter method for performance 
        public void FilterPerformance(string performance)
        {
            SearchByPerformance(performance);


        }

        public ObservableCollection<Researcher> GetViewableList()
        {
            return VisibleResearcher;
        }
        
        //Search by researcher's last name using LINQ & tutorial example
        public void SearchByName(string name)
        {
           
                var selected = from Researcher r in researcherList
                               where
                               r.FamilyName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0
                               || r.GivenName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0

                               select r;
                

            viewableResearcher.Clear();
            selected.ToList().ForEach(viewableResearcher.Add);



        }


        //Filetering researchers by level using LINQ 


        public void SearchByLevel(EmploymentLevel level)
        {
            var selected = from Researcher r in researcherList
                           where r.Level == level || level == EmploymentLevel.All
                           select r;
            viewableResearcher.Clear();
            selected.ToList().ForEach(viewableResearcher.Add);
        }


        //because parsing emums to strings is shocking~!
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public void SearchByPerformance(String Performance)
        {
            var selected = from Researcher r in researcherList
                           where r.performanceS == Performance
                           select r;
            viewableResearcher.Clear();
            selected.ToList().ForEach(viewableResearcher.Add);
        }
    }
}
