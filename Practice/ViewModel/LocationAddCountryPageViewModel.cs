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
    public class LocationAddCountryPageViewModel : MVVMModel
    {
        public ObservableCollection<CountryModel> Countries { get; set; } = new ObservableCollection<CountryModel>();

        public CountryModel SelectedCountry { get; set; }

        public LocationModel SelectedLocation { get;  }

        private Window window;

        public LocationAddCountryPageViewModel(LocationModel selectedLocation, Window w)
        {
            List<Country> CountriesList = CountryService.GetCountries();
            this.SelectedLocation = selectedLocation;
            foreach (Country c in CountriesList)
                Countries.Add(new CountryModel(c));
            window = w;
        }

        private RelayCommand addCountry;
        public RelayCommand AddCountry
        {
            get
            {
                return addCountry ??
                    (addCountry = new RelayCommand(obj =>
                    {
                        SelectedLocation.Country = SelectedCountry.Country;
                        window.Close();
                    },
                    (obj) => this.SelectedCountry != null));
            }
        }
    }
}
