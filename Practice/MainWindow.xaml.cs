using System;
using System.Collections;
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
using DatabaseConnector;
using Models;
using Practice.MVVMModels;
using Practice.ViewModel;


namespace Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserModel User { get; set; }
        public MainWindow(UserModel user)
        {
            ApplicationContext db = new ApplicationContext();

            InitializeComponent();

            User = user;
        }

        private void CountryTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Countries Tab открыта");
            this.DataContext = new ApplicationCountryViewModel();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (CountryTab.IsSelected)
            //    this.DataContext = new ApplicationCountryViewModel();

        }

        private void LocationTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DataContext = new ApplicationLocationViewModel();
        }

        private void ScientistTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DataContext = new ApplicationScientistViewModel();
        }

        private void TabConference_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Открываем конференции");
            this.DataContext = new ApplicationConferenceViewModel();
        }

        private void TabUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DataContext = new ApplicationUserViewModel();
        }

        private void TabReport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DataContext = new ApplicationReportViewModel();
        }

        private void TabOrganization_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DataContext = new ApplicationOrganizationViewModel();
        }

    }
}
