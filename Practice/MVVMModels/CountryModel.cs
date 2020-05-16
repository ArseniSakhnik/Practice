using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Models;
using DatabaseConnector;

namespace Practice.MVVMModels
{
    public class CountryModel : MVVMModel  
    {
        public Country Country { get; }
        private string StartedName { get; set; }

        public CountryModel(Country country)
        {
            this.Country = country;
            this.StartedName = country.CountryName;
        }

        public string CountryName
        {
            get
            {
                return Country.CountryName;
            }
            set
            {
                Country.CountryName = value;
                if (value.Length > 0)
                {  
                    CountryService.ChangeCountry(this.Country, this.StartedName);
                    StartedName = Country.CountryName;
                    OnPropertyChanged("CountryName");
                }

            }
        }
    }
}
