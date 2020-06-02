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
    public class UserChangeRoleViewModel : MVVMModel
    {
        public ObservableCollection<RoleModel> Roles { get; set; } = new ObservableCollection<RoleModel>();
        public Role SelectedRole { get; set; }
        public UserModel SelectedUser { get; }

        private Window window;

        public UserChangeRoleViewModel(UserModel user, Window window)
        {
            List<Role> roles = RoleService.GetRoles();
            foreach (Role r in roles)
                Roles.Add(new RoleModel(r));
            this.SelectedUser = user;
            this.window = window;

            Roles.CollectionChanged += (o, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    RoleModel rm = null;
                    foreach (RoleModel rom in e.NewItems)
                        rm = rom;
                    UserService.AddRole(rm.Role);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    RoleModel rm = null;
                    foreach (RoleModel rom in e.OldItems)
                        rm = rom;
                    UserService.RemoveRole(rm.Role);
                }
            };
        }

        public RelayCommand changeRoleCommand;
        public RelayCommand ChangeRoleCommand
        {
            get
            {
                return changeRoleCommand ??
                    (changeRoleCommand = new RelayCommand(obj =>
                    {
                        SelectedUser.Role = SelectedRole;
                        window.Close();
                    },
                    (obj) => SelectedRole != null));
            }
        }
    }
}
