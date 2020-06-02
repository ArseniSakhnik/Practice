using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Models;
using DatabaseConnector;

namespace Practice.MVVMModels
{
    public class UserModel : MVVMModel
    {
        public User User { get; }

        public UserModel(User user)
        {
            User = user;
        }

        public string UserName 
        {
            get => User.UserName;
            set
            {
                this.User.UserName = value;
                if (value.Length > 0)
                    UserService.ChangeUser(User);
                OnPropertyChanged("UserName");
            }
        }

        public string Login
        {
            get => User.Login;
            set
            {
                this.User.Login = value;
                if (value.Length > 0)
                    UserService.ChangeUser(User);
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get => User.Password;
            set
            {
                this.User.Password = value;
                if (value.Length > 0)
                    UserService.ChangeUser(User);
                OnPropertyChanged("Password");
            }
        }

        public string RoleName
        {
            get =>  this.User.Role.Name;
            set
            {

            }
        }

        public Role Role
        {
            get => User.Role;
            set
            {
                User.Role = value;
                OnPropertyChanged("RoleName");
            }
        }
        
    }
}
