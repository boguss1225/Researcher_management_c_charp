using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Model;
using System.Collections.ObjectModel;

namespace Controller
{
    class MiniResearcherController
    {
        public List<Researcher> researcherList;
        public List<Researcher> Workers { get { return researcherList; } set { } }
        public List<string> EmailList;
        private ObservableCollection<Researcher> viewableResearcher;
        public ObservableCollection<Researcher> VisibleResearcher { get { return viewableResearcher; } set { } }



        public MiniResearcherController(List<Researcher> researcherListP) {
            researcherList = researcherListP;
            EmailList = new List<string>() { };
            viewableResearcher = new ObservableCollection<Researcher>(researcherList);
        }

        public ObservableCollection<Researcher> GetViewableList()
        {
            return VisibleResearcher;
        }

        //filter method for performance 
        public void FilterPerformance(string performance)
        {
            SearchByPerformance(performance);


        }

        public void SearchByPerformance(String Performance)
        {
            var selected = from Researcher r in researcherList
                           where r.performanceS == Performance
                           select r;
            viewableResearcher.Clear();
            selected.ToList().ForEach(viewableResearcher.Add);
            
        }

        public void SearchForEmail(String Performance)
        {
            var selected = from Researcher r in researcherList
                           where r.performanceS == Performance
                           select r.Email;
            EmailList.Clear();
            selected.ToList().ForEach(EmailList.Add);
        }


    }
}