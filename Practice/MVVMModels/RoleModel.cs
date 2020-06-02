using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Models;
using DatabaseConnector;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;

namespace Practice.MVVMModels
{
    public class RoleModel : MVVMModel
    {
        public Role Role { get; }
        public RoleModel()
        {
            this.Role = new Role { Name = UserService.RoleCount().ToString() };
        }
        public RoleModel(Role role)
        {
            Role = role;
        }

        public string Name
        {
            get => Role.Name;
            set
            {
                Role.Name = value.ToUpper();
                if (value.Length > 0)
                    UserService.ChangeRole(Role);
            }
        }

        public bool IsReportsAvailable
        {
            get => Role.IsReportsAvailable;
            set
            {
                Role.IsReportsAvailable = value;
                UserService.ChangeRole(Role);
            }
        }

        public bool IsOrganizationAvailable
        {
            get => Role.IsOrganizationAvailable;
            set
            {
                Role.IsOrganizationAvailable = value;
                UserService.ChangeRole(Role);
            }
        }

        public bool IsConfereceAvailable
        {
            get => Role.IsConfereceAvailable;
            set
            {
                Role.IsConfereceAvailable = value;
                UserService.ChangeRole(Role);
            }
        }

        public bool IsScientistAvailable
        {
            get => Role.IsScientistAvailable;
            set
            {
                Role.IsScientistAvailable = value;
                UserService.ChangeRole(Role);
            }
        }

        public bool IsLocalityAvailable
        {
            get => Role.IsLocalityAvailable;
            set
            {
                Role.IsLocalityAvailable = value;
                UserService.ChangeRole(Role);
            }
        }

        public bool IsUserAvialble
        {
            get => Role.IsUserAvialble;
            set
            {
                Role.IsUserAvialble = value;
                UserService.ChangeRole(Role);
            }
        }

        public bool IsWordReportAvailable
        {
            get => Role.IsWordReportAvailable;
            set
            {
                Role.IsWordReportAvailable = value;
                UserService.ChangeRole(Role);
            }
        }

        public bool IsCountryAvailable
        {
            get => Role.IsCountryAvailable;
            set
            {
                Role.IsCountryAvailable = value;
                UserService.ChangeRole(Role);
            }
        }


    }
}
