using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Practice.MVVMModels;
using DatabaseConnector;
using System.Linq;
using Models;
using Practice.Commands;
using System.Collections;
using System.Windows.Controls;

namespace Practice.ViewModel
{
    public class ApplicationConferenceViewModel : MVVMModel
    {
        private ConferenceModel selectedConference;

        public ConferenceModel SelectedConference
        {
            get => selectedConference;
            set
            {
                this.selectedConference = value;
                OnPropertyChanged("SelectedConference");
            }
        }

        public ObservableCollection<ConferenceModel> Conferences { get; set; } = new ObservableCollection<ConferenceModel>();

        private RelayCommand addConferenceCommand;

        public RelayCommand AddConferenceCommand
        {
            get
            {
                return addConferenceCommand ??
                    (addConferenceCommand = new RelayCommand(obj =>
                    {
                        Conference conferece = new Conference
                        {
                            ConferenceName = "Имя конференции " + (Conferences.Count + 1),
                            ConferenceDescription = "Описание конференции " + (Conferences.Count + 1),

                        };

                        ConferenceModel cm = new ConferenceModel(conferece);
                        ConferenceService.AddConference(conferece);
                        Conferences.Insert(0, cm);
                        selectedConference = cm;
                    },
                    (obj) =>
                    {
                        if (selectedConference != null)
                        {
                            IEnumerable<ConferenceModel> a = (from c in Conferences where c.ConferenceName.Length == 0 select c);
                            if (a.Count() > 0)
                            {
                                return false;
                            }
                            else
                                return true;
                        }
                        return true;
                    }));

            }
        }

        private RelayCommand removeConferenceCommand;

        public RelayCommand RemoveConferenceCommand
        {
            get
            {
                return removeConferenceCommand ??
                    (removeConferenceCommand = new RelayCommand(obj =>
                    {
                        ConferenceModel cm = obj as ConferenceModel;
                        if (cm != null)
                            Conferences.Remove(cm);
                    },
                    (obj) => selectedConference != null));
            }
        }

        private RelayCommand findConferenceCommand;

        private RelayCommand changeLocationCommand;

        public RelayCommand ChangeLocationCommand
        {
            get => changeLocationCommand ?? (changeLocationCommand = new RelayCommand(obj =>
            {
                ConferenceChangesLocation conferenceChangesLocation = new ConferenceChangesLocation(SelectedConference);
                conferenceChangesLocation.Show();
            },
                (obj) => SelectedConference != null));
        }

        public RelayCommand FindConferenceCommand
        {
            get
            {
                return findConferenceCommand ??
                    (findConferenceCommand = new RelayCommand(obj =>
                    {
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            Conferences = new ObservableCollection<ConferenceModel>(Conferences.OrderByDescending(c => c.ConferenceName.Contains(obj.ToString())));
                            OnPropertyChanged("Conferences");
                        }
                        else if (obj != null)
                        {
                            Conferences = new ObservableCollection<ConferenceModel>(Conferences.OrderBy(c => c.ConferenceName));
                            OnPropertyChanged("Conferences");
                        }
                    }));
            }
        }


        private RelayCommand addScientistCommand;

        private RelayCommand removeScientistCommand;
        public RelayCommand RemoveScientistCommand
        {
            get
            {
                return removeScientistCommand ??
                    (removeScientistCommand = new RelayCommand(obj =>
                    {
                        selectedConference.Scientists.Remove(selectedConference.SelectedScientist);
                    },
                    (obj) => selectedConference != null && selectedConference.SelectedScientist != null
                    ));
            }
        }
        public RelayCommand AddScientistCommand
        {
            get
            {
                return addScientistCommand ??
                    (addScientistCommand = new RelayCommand(obj =>
                    {
                        ConferenceAddsScientist organizationAddsScientist = new ConferenceAddsScientist(selectedConference);
                        organizationAddsScientist.Show();
                    },
                    (obj) => selectedConference != null));
            }
        }

        public ApplicationConferenceViewModel()
        {
            List<Conference> conferences = ConferenceService.GetConferences();
            foreach (Conference c in conferences)
                Conferences.Add(new ConferenceModel(c));

            Conferences.CollectionChanged += (o, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    ConferenceModel conferenceModel = null;
                    foreach (ConferenceModel cm in e.NewItems)
                        conferenceModel = cm;

                    ConferenceService.AddConference(conferenceModel.Conference);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    ConferenceModel conferenceModel = null;
                    foreach (ConferenceModel cm in e.OldItems)
                        conferenceModel = cm;

                    ConferenceService.RemoveConference(conferenceModel.Conference);
                }
                OnPropertyChanged("Conferences");
            };

        }

        private RelayCommand conferencesWithoutPublishedMaterialsCommand;

        public RelayCommand ConferencesWithoutPublishedMaterialsCommand
        {
            get
            {
                return conferencesWithoutPublishedMaterialsCommand ??
                    (conferencesWithoutPublishedMaterialsCommand = new RelayCommand(obj =>
                    {
                        bool IsChecked = Convert.ToBoolean(obj);
                        if (IsChecked)
                        {
                            List<Conference> conferences = ConferenceService.GetUnpublishedConferences();
                            Conferences = new ObservableCollection<ConferenceModel>();
                            foreach(Conference c in conferences)
                                Conferences.Add(new ConferenceModel(c));
                            OnPropertyChanged("Conferences");
                        }
                        else
                        {
                            List<Conference> conferences = ConferenceService.GetConferences();
                            Conferences = new ObservableCollection<ConferenceModel>();
                            foreach (Conference c in conferences)
                                Conferences.Add(new ConferenceModel(c));
                            OnPropertyChanged("Conferences");
                        }

                    }));
            }
        }

        private RelayCommand sortByScientistsCommand;

        public RelayCommand SortByScientistCommand
        {
            get
            {
                return sortByScientistsCommand ??
                    (sortByScientistsCommand = new RelayCommand(obj =>
                    {
                        bool IsChecked = Convert.ToBoolean(obj);
                        if (IsChecked)
                        {
                            Conferences = new ObservableCollection<ConferenceModel>(Conferences.OrderByDescending(c => c.Conference.ScientistConference.Count));
                            OnPropertyChanged("Conferences");
                        }
                        else
                        {
                            Conferences = new ObservableCollection<ConferenceModel>(Conferences.OrderByDescending(c => c.ConferenceName));
                            OnPropertyChanged("Conferences");
                        }
                    }));
            }
        }

        private RelayCommand datePickerCommand;

        public RelayCommand DatePickerCommand
        {
            get
            {
                return datePickerCommand ??
                    (datePickerCommand = new RelayCommand(obj =>
                    {
                        DatePicker datePicker = new DatePicker(null, selectedConference);
                        datePicker.Show();
                    },
                    (obj) => selectedConference != null));
            }
        }



    }
}
