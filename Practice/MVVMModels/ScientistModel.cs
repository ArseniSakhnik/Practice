using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Models;
using DatabaseConnector;
using System.Collections.ObjectModel;

namespace Practice.MVVMModels
{
    public class ScientistModel : MVVMModel
    {
        public Scientist Scientist { get; }

        public ObservableCollection<ConferenceModel> Conferences { get; set; } = new ObservableCollection<ConferenceModel>();

        public ObservableCollection<ReportModel> Reports { get; set; } = new ObservableCollection<ReportModel>();

        public ObservableCollection<OrganizationModel> Organizations { get; set; } = new ObservableCollection<OrganizationModel>();

        public ConferenceModel SelectedConference { get; set; }

        public ReportModel SelectedReport { get; set; }

        public OrganizationModel SelectedOrganization { get; set; }

        public ScientistModel(Scientist scientist) { 
            Scientist = scientist;

            List<Conference> conferences = ScientistService.GetConferences(Scientist);
            foreach (Conference c in conferences)
                Conferences.Add(new ConferenceModel(c, false));

            foreach (Report r in Scientist.Reports)
                Reports.Add(new ReportModel(r));

            List<Organization> organizations = ScientistService.GetOrganizations(Scientist);
            foreach (Organization o in organizations)
                Organizations.Add(new OrganizationModel(o, false));

            Conferences.CollectionChanged += (o, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    ConferenceModel cm = null;
                    foreach (ConferenceModel cmo in e.NewItems)
                        cm = cmo;
                    ScientistService.AddConference(Scientist, cm.Conference);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    ConferenceModel cm = null;
                    foreach (ConferenceModel cmo in e.OldItems)
                        cm = cmo;
                    ScientistService.RemoveConference(Scientist, cm.Conference);
                }
                OnPropertyChanged("Conferences");
            };

            Reports.CollectionChanged += (o, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    ReportModel rm = null;
                    foreach (ReportModel rem in e.NewItems)
                        rm = rem;
                    ScientistService.AddReport(Scientist, rm.Report);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    ReportModel rm = null;
                    foreach (ReportModel rem in e.OldItems)
                        rm = rem;
                    ScientistService.RemoveReport(Scientist, rm.Report);
                }
                OnPropertyChanged("Reports");
            };

            Organizations.CollectionChanged += (o, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    OrganizationModel om = null;
                    foreach (OrganizationModel orm in e.NewItems)
                        om = orm;
                    ScientistService.AddOrganization(Scientist, om.Organization);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    OrganizationModel om = null;
                    foreach (OrganizationModel orm in e.OldItems)
                        om = orm;
                    ScientistService.RemoveOrganization(Scientist, om.Organization);
                }

                OnPropertyChanged("Organizations");
            };


        }

        public string ScientistFullName
        {
            get => Scientist.Name + " " + this.Scientist.LastName;
            set { }
        }

        public string ScientistName
        {
            get => Scientist.Name;
            set
            {
                Scientist.Name = value;
                if (value.Length > 0)
                {
                    ScientistService.ChangeScientist(this.Scientist);
                }
                OnPropertyChanged("ScientistName");
            }
        }

        public string ScientistLastName
        {
            get => Scientist.LastName;
            set
            {
                Scientist.LastName = value;
                if (value.Length > 0)
                {
                    ScientistService.ChangeScientist(this.Scientist);
                    OnPropertyChanged("ScientistLastName");
                }
            }
        }

        
    }
}
