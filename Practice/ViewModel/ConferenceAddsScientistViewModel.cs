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
    public class ConferenceAddsScientistViewModel : MVVMModel
    {
        public ObservableCollection<ScientistModel> Scientists { get; set; } = new ObservableCollection<ScientistModel>();
        public Window Window { get; set; }
        public ScientistModel SelectedScientist { get; set; }
        public ConferenceModel SelectedConference { get; }
        public ConferenceAddsScientistViewModel(Window window, ConferenceModel conference)
        {
            List<Scientist> scientists = ScientistService.GetScientists();
            foreach (Scientist s in scientists)
                Scientists.Add(new ScientistModel(s));
            this.Window = window;
            this.SelectedConference = conference;
        }

        private RelayCommand addScientistCommand;

        public RelayCommand AddScientistCommand
        {
            get
            {
                return addScientistCommand ??
                    (addScientistCommand = new RelayCommand(obj =>
                    {
                        SelectedConference.Scientists.Add(SelectedScientist);
                    },
                    (obj) => SelectedScientist != null));
            }
        }
    }
}
