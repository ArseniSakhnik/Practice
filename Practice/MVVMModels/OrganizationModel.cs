using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Models;
using DatabaseConnector;
using System.Collections.ObjectModel;

namespace Practice.MVVMModels
{
    public class OrganizationModel : MVVMModel
    {
        public Organization Organization { get; set; }
        public ObservableCollection<ScientistModel> Scientists { get; set; } = new ObservableCollection<ScientistModel>();
        public ScientistModel SelectedScientist { get; set; }

        public OrganizationModel(Organization organization, bool downloadEntityDates = true)
        {
            Organization = organization;
            if (downloadEntityDates)
            {
                List<Scientist> scientists = OrganizationService.GetScientists(Organization);
                foreach (Scientist s in scientists)
                    Scientists.Add(new ScientistModel(s));

                Scientists.CollectionChanged += (o, e) =>
                {
                    if (e.Action.ToString().Equals("Add"))
                    {
                        ScientistModel sm = null;
                        foreach (ScientistModel scm in e.NewItems)
                            sm = scm;
                        OrganizationService.AddScientist(Organization, sm.Scientist);

                    }
                    else if (e.Action.ToString().Equals("Remove"))
                    {
                        ScientistModel sm = null;
                        foreach (ScientistModel scm in e.OldItems)
                            sm = scm;
                        OrganizationService.RemoveScientist(Organization, sm.Scientist);
                    }
                    OnPropertyChanged("Scientists");
                };
            }
        }

        public string OrganizationName
        {
            get => Organization.OrganizationName;

            set
            {
                Organization.OrganizationName = value;
                if (value.Length > 0)
                    OrganizationService.ChangeOrganization(Organization);
                OnPropertyChanged("OrganizationName");
            }
        }
    }
}
