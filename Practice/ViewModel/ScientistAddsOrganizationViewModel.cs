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
    public class ScientistAddsOrganizationViewModel : MVVMModel
    {
        public ObservableCollection<OrganizationModel> Organizations { get; set; } = new ObservableCollection<OrganizationModel>();

        public OrganizationModel SelectedOrganization { get; set; }

        public ScientistModel SelectedScientist { get; set; }

        public ScientistAddsOrganizationViewModel(ScientistModel selectedScientist)
        {
            SelectedScientist = selectedScientist;
            List<Organization> conferences = OrganizationService.GetOrganizations();
            foreach (Organization o in conferences)
                Organizations.Add(new OrganizationModel(o));
        }

        private RelayCommand addOrganizationCommand;

        public RelayCommand AddOrganizationCommand
        {
            get
            {
                return addOrganizationCommand ??
                    (addOrganizationCommand = new RelayCommand(obj =>
                    {
                        SelectedScientist.Organizations.Add(SelectedOrganization);
                    },
                    (obj) => SelectedOrganization != null));
            }
        }
    }
}
