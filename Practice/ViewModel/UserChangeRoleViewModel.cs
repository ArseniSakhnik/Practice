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
        public ObservableCollection<Role> Roles { get; set; } = new ObservableCollection<Role>();
        public Role SelectedRole { get; set; }
        public UserModel SelectedUser { get; }

        private Window window;

        public UserChangeRoleViewModel(UserModel user, Window window)
        {
            List<Role> roles = RoleService.GetRoles();
            foreach (Role r in roles)
                Roles.Add(r);
            this.SelectedUser = user;
            this.window = window;
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
