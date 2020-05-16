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
    public class ScientistAddsConferenceViewModel : MVVMModel
    {
        public ObservableCollection<ConferenceModel> Conferences { get; set; } = new ObservableCollection<ConferenceModel>();

        public ConferenceModel SelectedConference { get; set; }

        public ScientistModel SelectedScientist { get; set; }

        public ScientistAddsConferenceViewModel(ScientistModel selectedScientist)
        {
            SelectedScientist = selectedScientist;
            List<Conference> conferences = ConferenceService.GetConferences();
            foreach (Conference c in conferences)
                Conferences.Add(new ConferenceModel(c));
        }

        private RelayCommand addConferenceCommand;

        public RelayCommand AddConferenceCommand
        {
            get
            {
                return addConferenceCommand ??
                    (addConferenceCommand = new RelayCommand(obj =>
                    {
                        SelectedScientist.Conferences.Add(SelectedConference);
                    },
                    (obj) => SelectedConference != null));
            }
        }
    }
}
