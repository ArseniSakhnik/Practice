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
    public class ApplicationScientistViewModel : MVVMModel
    {
        private ScientistModel selectedScientist;
        public ScientistModel SelectedScientist
        {
            get
            {
                return selectedScientist;
            }
            set
            {
                selectedScientist = value;
                ReportsCount = "Доклады (" + (selectedScientist.Reports.Count + 1) + "):";
                OnPropertyChanged("SelectedScientist");
            }
        }

        public string reportsCount = "Доклады";

        public string ReportsCount
        {
            get => reportsCount;
            set
            {
                reportsCount = value;
                OnPropertyChanged("ReportsCount");
            }
        }

        public ObservableCollection<ScientistModel> Scientists { get; set; } = new ObservableCollection<ScientistModel>();

        private RelayCommand addScientistCommand;

        public RelayCommand AddScientistCommand
        {
            get
            {
                return addScientistCommand ??
                    (addScientistCommand = new RelayCommand(obj =>
                    {
                        Scientist scientist = new Scientist
                        {
                            Name = "Имя ученого " + (Scientists.Count() + 1),
                            LastName = "Фамилия ученого " + (Scientists.Count() + 1)
                        };

                        ScientistModel sc = new ScientistModel(scientist, false);
                        ScientistService.AddScientist(scientist);
                        Scientists.Insert(0, sc);
                        selectedScientist = sc;
                    },
                    (obj) =>
                    {
                        if (selectedScientist != null)
                        {
                            IEnumerable<ScientistModel> a = (from s in Scientists where s.ScientistName.Length == 0 select s);
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

        private RelayCommand removeScientistCommand;

        public RelayCommand RemoveScientistCommand
        {
            get
            {
                return removeScientistCommand ??
                    (removeScientistCommand = new RelayCommand((obj) =>
                    {
                        ScientistModel sm = obj as ScientistModel;
                        if (sm != null)
                        {
                            Scientists.Remove(sm);
                        }
                    },
                    (obj) => Scientists.Count > 0));
            }
        }

        private RelayCommand findScientistCommand;

        public RelayCommand FindScientistCommand
        {
            get
            {
                return findScientistCommand ??
                    (findScientistCommand = new RelayCommand((obj) =>
                    {
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            Scientists = new ObservableCollection<ScientistModel>(Scientists.OrderByDescending(i => i.ScientistFullName.Contains(obj.ToString())));
                            OnPropertyChanged("Scientists");
                        }
                        else if (obj != null)
                        {
                            Scientists = new ObservableCollection<ScientistModel>(Scientists.OrderBy(i => i.ScientistName));
                            OnPropertyChanged("Scientists");
                        }
                    }));
            }
        }

        private RelayCommand addReportCommand;

        public RelayCommand AddReportCommand
        {
            get
            {
                return addReportCommand ??
                    (addReportCommand = new RelayCommand(obj =>
                    {
                        ScientistAddsReport scientistAddsReport = new ScientistAddsReport(selectedScientist);
                        scientistAddsReport.Show();
                    },
                    (obj) => selectedScientist != null));
            }
        }

        private RelayCommand removeReportCommand;

        public RelayCommand RemoveReportCommand
        {
            get
            {
                return removeReportCommand ??
                    (removeReportCommand = new RelayCommand(obj =>
                    {
                        selectedScientist.Reports.Remove(selectedScientist.SelectedReport);
                    },
                    obj => selectedScientist != null && selectedScientist.SelectedReport != null));
            }
        }

        private RelayCommand addConferenceCommand;

        public RelayCommand AddConferenceCommand
        {
            get
            {
                return addConferenceCommand ??
                    (addConferenceCommand = new RelayCommand(obj =>
                    {
                        ScientistAddsConference scientistAddsConference = new ScientistAddsConference(SelectedScientist);
                        scientistAddsConference.Show();
                    },
                    (obj) => selectedScientist != null));
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
                        SelectedScientist.Conferences.Remove(SelectedScientist.SelectedConference);
                    },
                    (obj) => SelectedScientist != null && SelectedScientist.SelectedConference != null));
            }
        }

        private RelayCommand addOrganizationCommand;

        public RelayCommand AddOrganizationCommand
        {
            get
            {
                return addOrganizationCommand ??
                    (addOrganizationCommand = new RelayCommand(obj =>
                    {
                        ScientistAddsOrganization scientistAddsOrganization = new ScientistAddsOrganization(SelectedScientist);
                        scientistAddsOrganization.Show();

                    },
                    (obj) => SelectedScientist != null));
            }
        }

        private RelayCommand removeOrganizationCommand;

        public RelayCommand RemoveOrganizationCommand
        {
            get
            {
                return removeOrganizationCommand ??
                    (removeOrganizationCommand = new RelayCommand(obj =>
                    {
                        SelectedScientist.Organizations.Remove(SelectedScientist.SelectedOrganization);
                    },
                    (obj) => SelectedScientist != null && SelectedScientist.SelectedOrganization != null ));
            }
        }


        public ApplicationScientistViewModel()
        {
            List<Scientist> scientists = ScientistService.GetScientists();
            foreach (Scientist s in scientists)
            {
                Scientists.Add(new ScientistModel(s));
            }

            Scientists.CollectionChanged += (o, e) =>
                {
                    if (e.Action.ToString().Equals("Add"))
                    {
                        ScientistModel scientistModel = null;
                        foreach (ScientistModel sm in e.NewItems)
                            scientistModel = sm;
                        ScientistService.AddScientist(scientistModel.Scientist);

                    }
                    else if (e.Action.ToString().Equals("Remove"))
                    {
                        ScientistModel scientistModel = null;
                        foreach (ScientistModel sm in e.OldItems)
                            scientistModel = sm;

                        ScientistService.RemoveScientist(scientistModel.Scientist);
                    }
                    OnPropertyChanged("Scientists");
                };
        }
    }
}
