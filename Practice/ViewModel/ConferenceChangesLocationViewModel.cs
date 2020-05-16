using DatabaseConnector;
using Models;
using Practice.Commands;
using Practice.MVVMModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Practice.ViewModel
{
    public class ConferenceChangesLocationViewModel
    {
        public ObservableCollection<LocationModel> Locations { get; set; } = new ObservableCollection<LocationModel>();

        public LocationModel SelectedLocation { get; set; }

        public ConferenceModel SelectedConference { get; set; }

        public Window Window { get; set; } 

        public ConferenceChangesLocationViewModel(Window window, ConferenceModel selectedConference)
        {
            Window = window;
            SelectedConference = selectedConference;
            List<Location> locations = LocationService.GetLocations();
            foreach (Location l in locations)
                Locations.Add(new LocationModel(l));
        }

        private RelayCommand changeLocationCommand;

        public RelayCommand ChangeLocationCommand
        {
            get
            {
                return changeLocationCommand ??
                    (changeLocationCommand = new RelayCommand(obj =>
                    {
                        //SelectedOrganization
                        SelectedConference.Location = SelectedLocation.Location;
                        Window.Close();
                    },
                    (obj) => SelectedLocation != null));
            }
        }
    }
}
