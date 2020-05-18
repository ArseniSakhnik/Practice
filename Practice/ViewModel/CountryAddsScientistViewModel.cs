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
    public class CountryAddsScientistViewModel
    {
        public ObservableCollection<ScientistModel> Scientists { get; set; } = new ObservableCollection<ScientistModel>();
        public Window Window { get; set; }
        public ScientistModel SelectedScientist { get; set; }
        public CountryModel SelectedCountry { get; }
        public CountryAddsScientistViewModel(Window window, CountryModel selectedCountry)
        {
            List<Scientist> scientists = ScientistService.GetScientists();
            foreach (Scientist s in scientists)
                Scientists.Add(new ScientistModel(s));
            this.Window = window;
            this.SelectedCountry = selectedCountry;
        }

        private RelayCommand addScientistCommand;

        public RelayCommand AddScientistCommand
        {
            get
            {
                return addScientistCommand ??
                    (addScientistCommand = new RelayCommand(obj =>
                    {
                        SelectedScientist.Country = SelectedCountry.Country;
                        Window.Close();
                    },
                    obj => SelectedScientist != null));
            }
        }
    }
}
