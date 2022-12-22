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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using Model;
using Database;



namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const string Key = "researcherList";
        private ResearcherController researcherController;
        public MainWindow()
        {
            
            InitializeComponent();
            researcherController = (ResearcherController)(Application.Current.FindResource(Key) as ObjectDataProvider).ObjectInstance;
            //CumulitiveCount.Visibility = System.Windows.Visibility.Hidden;
            
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
           
        }
   
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (e.AddedItems.Count > 0)
            {
                DetailsPanel.DataContext = e.AddedItems[0];
                CumulitiveCount.DataContext = e.AddedItems[0];
            }
            
        }


      

        private void button_Click(object sender, RoutedEventArgs e) //Reverse the publication list
        {
            if (listbox_Researcher.SelectedItem == null)
            {

                MessageBox.Show("No Publications");
            }
            else
            {
                List<Publication> lstPub = ((Researcher)listbox_Researcher.SelectedItem).Publi;
                lstPub.Reverse();
                listbox_Publication.ItemsSource = null;
                listbox_Publication.ItemsSource = lstPub;
            }

        }

        private void listbox_Publication_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (researcherController == null)
            {

            }
            else
            {
                PublicationPanel.DataContext = listbox_Publication.SelectedItem;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SupervisionName sun = new SupervisionName();
            sun.DataContext = showName.DataContext;
            sun.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (CumulitiveCount.Visibility == System.Windows.Visibility.Hidden)
            {
                CumulitiveCount.Visibility = System.Windows.Visibility.Visible;
            }
            else
                CumulitiveCount.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Usercontrol_Loaded(object sender, RoutedEventArgs e)
        {

        }

        //roport button
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ReportView reportView = new ReportView(researcherController.Workers);
            reportView.ShowDialog();
        }
    }
}
