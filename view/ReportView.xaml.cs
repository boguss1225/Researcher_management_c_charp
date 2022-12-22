using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controller;
using Model;

namespace View
{
    public partial class ReportView : Window
    {
        private MiniResearcherController researcherController1 { get; set; }
        private MiniResearcherController researcherController2 { get; set; }
        private MiniResearcherController researcherController3 { get; set; }
        private MiniResearcherController researcherController4 { get; set; }
      
        public ReportView(List<Researcher> researcherList)
        {
            InitializeComponent();
            researcherController1 = new MiniResearcherController(researcherList);
            researcherController2 = new MiniResearcherController(researcherList);
            researcherController3 = new MiniResearcherController(researcherList);
            researcherController4 = new MiniResearcherController(researcherList);

            researcherController1.FilterPerformance("Poor");
            PoorP.DataContext = researcherController1.GetViewableList();

            researcherController2.FilterPerformance("Below Expectations");
            BelowP.DataContext = researcherController2.GetViewableList();

            researcherController3.FilterPerformance("Meeting Minimum");
            MeetingP.DataContext = researcherController3.GetViewableList();

            researcherController4.FilterPerformance("Star Performers");
            StarP.DataContext = researcherController4.GetViewableList();

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //poor btn
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            researcherController1.SearchForEmail("Poor");
            var result = String.Join("; ", researcherController1.EmailList.ToArray());
            Clipboard.SetText(result);
        }

        //starPerformers btn
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            researcherController1.SearchForEmail("Star Performers");
            var result = String.Join("; ", researcherController1.EmailList.ToArray());
            Clipboard.SetText(result);
        }

        //meeting btn
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            researcherController1.SearchForEmail("Meeting Minimum");
            var result = String.Join("; ", researcherController1.EmailList.ToArray());
            Clipboard.SetText(result);
        }

        //Below btn
        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            researcherController1.SearchForEmail("Below Expectations");
            var result = String.Join("; ", researcherController1.EmailList.ToArray());
            Clipboard.SetText(result);
        }

       
    }
}
