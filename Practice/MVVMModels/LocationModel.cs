using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Models;
using DatabaseConnector;

namespace Practice.MVVMModels
{
    public class LocationModel : MVVMModel
    {
        public Location Location { get; }

        private string StartedName { get; set; }

        public LocationModel(Location location)
        {
            this.Location = location;
            this.StartedName = location.LocationName;
        }
        public string LocationName
        {
            get => Location.LocationName;
            set
            {
                Location.LocationName = value;
                if (value.Length > 0)
                {
                    LocationService.ChangeLocation(this.Location, this.StartedName);
                    StartedName = Location.LocationName;
                    OnPropertyChanged("LocationName");
                }
            }
        }

        public string LocationDescription
        {
            get => Location.LocationDescription;
            set
            {
                this.Location.LocationDescription = value;
                LocationService.ChangeLocation(this.Location, this.StartedName);
                OnPropertyChanged("LocationDescription");
            }
        }

        public Country Country
        {
            get => Location.Country;
            set
            {
                Console.WriteLine("Привязываем свойство объекта");
                this.Location.Country = value;
                LocationService.ChangeLocation(this.Location, this.StartedName);
                OnPropertyChanged("Country");
                OnPropertyChanged("CountryName");
            }
        }

        public string CountryName
        {
            get => Location.Country.CountryName;
            set { }
        }

        public void AddConference(Conference conference)
        {
            LocationService.AddConference(this.Location, conference);
        }
    }
}
