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
using System.Collections.Generic;
using System;

namespace Practice.ViewModel
{
    public class ApplicationOrganizationViewModel : MVVMModel
    {
        private OrganizationModel selectedOrganization;

        private ScientistModel selectedScientist;
        public ScientistModel SelectedScientist
        {
            get => selectedScientist;
            set
            {
                selectedScientist = value;
                OnPropertyChanged("SelectedScientist");
            }
                
        }
        public OrganizationModel SelectedOrganization
        {
            get => selectedOrganization;
            set
            {
                selectedOrganization = value;
                OnPropertyChanged("SelectedOrganization");
            }
        }
        public ObservableCollection<OrganizationModel> Organizations { get; set; } = new ObservableCollection<OrganizationModel>();

        private RelayCommand addOrganizationCommand;
        public RelayCommand AddOrganizationCommand
        {
            get
            {
                return addOrganizationCommand ??
                    (addOrganizationCommand = new RelayCommand(obj =>
                    {
                        Organization organization = new Organization { OrganizationName = "Название организации " + (Organizations.Count + 1) };
                        OrganizationModel om = new OrganizationModel(organization, false);
                        OrganizationService.AddOrganization(organization);
                        Organizations.Insert(0, om);
                        selectedOrganization = om;
                    },
                    (obj) =>
                    {
                        if (selectedOrganization != null)
                        {
                            IEnumerable<OrganizationModel> a = (from c in Organizations where c.OrganizationName.Length == 0 select c);
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

        private RelayCommand removeOrganizationCommand;
        public RelayCommand RemoveOrganizationCommand
        {
            get
            {
                return removeOrganizationCommand ??
                    (removeOrganizationCommand = new RelayCommand(obj =>
                    {
                        OrganizationModel om = obj as OrganizationModel;
                        if (om != null)
                            Organizations.Remove(om);
                    },
                    (obj) => selectedOrganization != null));
            }
        }

        private RelayCommand findOrganizationCommand;
        public RelayCommand FindOrganizationCommand
        {
            get
            {
                return findOrganizationCommand ??
                    (findOrganizationCommand = new RelayCommand(obj =>
                    {
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            Organizations = new ObservableCollection<OrganizationModel>(Organizations.OrderByDescending(c => c.OrganizationName.Contains(obj.ToString())));
                            OnPropertyChanged("Conferences");
                        }
                        else if (obj != null)
                        {
                            Organizations = new ObservableCollection<OrganizationModel>(Organizations.OrderBy(c => c.OrganizationName));
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
                        selectedOrganization.Scientists.Remove(selectedOrganization.SelectedScientist);
                    },
                    (obj) => selectedOrganization != null && selectedOrganization.SelectedScientist != null
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
                        OrganizationAddsScientist organizationAddsScientist = new OrganizationAddsScientist(selectedOrganization);
                        organizationAddsScientist.Show();
                    },
                    (obj) => selectedOrganization != null));
            }
        }
        public ApplicationOrganizationViewModel()
        {
            List<Organization> organizations = OrganizationService.GetOrganizations();
            foreach (Organization c in organizations)
                Organizations.Add(new OrganizationModel(c));

            Organizations.CollectionChanged += (o, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    OrganizationModel organizationModel = null;
                    foreach (OrganizationModel om in e.NewItems)
                        organizationModel = om;

                    OrganizationService.AddOrganization(organizationModel.Organization);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    OrganizationModel organizationModel = null;
                    foreach (OrganizationModel om in e.OldItems)    
                        organizationModel = om;

                    OrganizationService.RemoveOrganization(organizationModel.Organization);
                }
                OnPropertyChanged("Organizations");
            };

            
        }
    }
}
