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
    public class ApplicationUserViewModel : MVVMModel
    {
        private UserModel selectedUser;

        public UserModel SelectedUser
        {
            get => this.selectedUser;

            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public ObservableCollection<UserModel> Users { get; set; } = new ObservableCollection<UserModel>();

        private RelayCommand addUserCommand;

        public RelayCommand AddUserCommand
        {
            get
            {
                return addUserCommand ??
                    (addUserCommand = new RelayCommand(obj =>
                    {
                        User user = new User
                        {
                            UserName = "Имя пользователя " + (Users.Count + 1),
                            Login = "Login пользователя " + (Users.Count + 1),
                            Password = "password"

                        };

                        UserModel um = new UserModel(user);
                        UserService.AddUser(user);
                        Users.Insert(0, um);
                        selectedUser = um;
                    },
                    (obj) =>
                    {
                        if (selectedUser != null)
                        {
                            IEnumerable<UserModel> a = (from u in Users where u.UserName.Length == 0 select u);
                            if (a.Count() > 0)
                            {
                                Console.WriteLine("Заполните  пропуски");
                                return false;
                            }
                            else
                                return true;
                        }
                        return true;
                    }));
            }
        }

        private RelayCommand removeUserCommand;

        public RelayCommand RemoveUserCommand
        {
            get
            {
                return removeUserCommand ??
                    (removeUserCommand = new RelayCommand(obj =>
                    {
                        UserModel um = obj as UserModel;
                        if (um != null)
                            Users.Remove(um);
                    },
                    (obj) => Users.Count > 0));
            }
        }

        private RelayCommand findUserCommand;

        private RelayCommand changeRoleCommand;
        public RelayCommand ChangeRoleCommand
        {
            get
            {
                return changeRoleCommand ??
                    (changeRoleCommand = new RelayCommand(obj =>
                    {
                        UserChangeRolePage userChangeRolePage = new UserChangeRolePage(selectedUser);
                        userChangeRolePage.Show();
                    },
                    (obj) => selectedUser != null));
            }
        }
        public RelayCommand FindUserCommand
        {
            get
            {
                return findUserCommand ??
                    (findUserCommand = new RelayCommand(obj =>
                    {
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            Console.WriteLine(obj.GetType() + " " + obj);
                            Users = new ObservableCollection<UserModel>(Users.OrderByDescending(u => u.UserName.Contains(obj.ToString())));
                            OnPropertyChanged("Users");
                        }
                        else if (obj != null)
                        {
                            Users = new ObservableCollection<UserModel>(Users.OrderBy(u => u.UserName));
                            OnPropertyChanged("Users");
                        }
                    }));
            }
        }   
        public ApplicationUserViewModel()
        {
            List<User> users = UserService.GetUsers();
            foreach (User u in users)
                Users.Add(new UserModel(u));

            Users.CollectionChanged += (o, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    UserModel userModel = null;
                    foreach (UserModel um in e.NewItems)
                        userModel = um;

                    UserService.AddUser(userModel.User);
                    Console.WriteLine("Добавили элемент " + userModel?.UserName);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    UserModel userModel = null;
                    foreach (UserModel um in e.OldItems)
                        userModel = um;

                    UserService.RemoveUser(userModel.User);
                    Console.WriteLine("Удаление элемента " + userModel?.UserName);
                }
                OnPropertyChanged("Users");
            };
        }
    }
}
