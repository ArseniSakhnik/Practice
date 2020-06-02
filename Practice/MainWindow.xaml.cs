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



            if (User.Role.Name != "ADMIN")
                UserTab.Visibility = Visibility.Hidden;

            if (!User.Role.IsConfereceAvailable)
                ConferenceTab.Visibility = Visibility.Hidden;

            if (!User.Role.IsCountryAvailable)
                CountryTab.Visibility = Visibility.Hidden;

            if (!User.Role.IsLocalityAvailable)
                LocationTab.Visibility = Visibility.Hidden;

            if (!User.Role.IsOrganizationAvailable)
                OrganizationTab.Visibility = Visibility.Hidden;

            if (!User.Role.IsReportsAvailable)
                ReportTab.Visibility = Visibility.Hidden;

            if (!User.Role.IsScientistAvailable)
                ScientistTab.Visibility = Visibility.Hidden;

            if (!User.Role.IsWordReportAvailable)
                WordReportTab.Visibility = Visibility.Hidden;

            if (!User.Role.IsUserAvialble)
                UserTab.Visibility = Visibility.Hidden;

        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryTab.IsSelected && !(DataContext is ApplicationCountryViewModel))
                DataContext = new ApplicationCountryViewModel();
            else if (LocationTab.IsSelected && !(DataContext is ApplicationLocationViewModel))
                DataContext = new ApplicationLocationViewModel();
            else if (ScientistTab.IsSelected && !(DataContext is ApplicationScientistViewModel))
                DataContext = new ApplicationScientistViewModel();
            else if (ConferenceTab.IsSelected && !(DataContext is ApplicationConferenceViewModel))
                DataContext = new ApplicationConferenceViewModel();
            else if (OrganizationTab.IsSelected && !(DataContext is ApplicationOrganizationViewModel))
                DataContext = new ApplicationOrganizationViewModel();
            else if (UserTab.IsSelected && !(DataContext is ApplicationUserViewModel))
                DataContext = new ApplicationUserViewModel();
            else if (ReportTab.IsSelected && !(DataContext is ApplicationReportViewModel))
                DataContext = new ApplicationReportViewModel();
            else if (WordReportTab.IsSelected && !(DataContext is ApplicationWordReportViewModel))
                DataContext = new ApplicationWordReportViewModel();
        }
    }
}
