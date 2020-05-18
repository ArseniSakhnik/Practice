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
    public class ConferenceModel : MVVMModel
    {
        public Conference Conference { get; set; }

        public ScientistModel SelectedScientist { get; set; }

        public ObservableCollection<ScientistModel> Scientists { get; set; } = new ObservableCollection<ScientistModel>();

        public ConferenceModel(Conference conference, bool downloadEntityDates = true)
        {
            Conference = conference;

            if (downloadEntityDates)
            {
                List<Scientist> scientists = ConferenceService.GetScientists(Conference);
                foreach (Scientist s in scientists)
                    Scientists.Add(new ScientistModel(s));

                Scientists.CollectionChanged += (o, e) =>
                {
                    if (e.Action.ToString().Equals("Add"))
                    {
                        ScientistModel sm = null;
                        foreach (ScientistModel scm in e.NewItems)
                            sm = scm;
                        ConferenceService.AddScientist(Conference, sm.Scientist);
                    }
                    else if (e.Action.ToString().Equals("Remove"))
                    {
                        ScientistModel sm = null;
                        foreach (ScientistModel scm in e.OldItems)
                            sm = scm;
                        ConferenceService.RemoveScientist(Conference, sm.Scientist);
                    }
                };
            }
            
        }

        public string ConferenceName
        {
            get => Conference.ConferenceName;
            set
            {
                Conference.ConferenceName = value;
                if (value.Length > 0)
                {
                    ConferenceService.ChangeConference(this.Conference);
                }
                OnPropertyChanged("ConferenceName");
            }
        }

        public string ConferenceDescription
        {
            get => Conference.ConferenceDescription;
            set
            {
                if (value.Length > 0)
                    ConferenceService.ChangeConference(this.Conference);
                OnPropertyChanged("ConferenceDescription");
            }
        }

        public DateTime StartOfConference
        {
            get => Conference.StartOfConference;
            set
            {
                Conference.StartOfConference = value;
                ConferenceService.ChangeConference(Conference);
                OnPropertyChanged("StartOfConference");
            }
        }

        public string LocationName
        {
            get => Conference.Location.LocationName;
            set { }
        }

        public Location Location
        {
            get => Conference.Location;
            set
            {
                Conference.Location = value;
                ConferenceService.ChangeConference(Conference);
                OnPropertyChanged("LocationName");
            }
        }

        public string CountryName
        {
            get => Conference.Location.Country.CountryName;
            set { }
        }

        public int NumberOfCountries
        {
            get
            {
                return ConferenceService.GetCountriesOnConference(Conference).Count;
            }
            set
            {

            }
        }

        



    }
}
