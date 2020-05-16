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
    public class AuthorizationViewModel : MVVMModel
    {

        public string Login { get; set; }

        public string Password { get; set; }

        public Window Window { get; set; }

        public AuthorizationViewModel(Window window)
        {
            Window = window;
        }

        private RelayCommand authorizationCommmand;

        public RelayCommand AuthorizatonCommand
        {
            get
            {
                return authorizationCommmand ??
                    (authorizationCommmand = new RelayCommand(obj =>
                    {
                        User user = UserService.GetUserByLoginAndPassword(Login, Password);

                        if (user != null)
                        {
                            MainWindow mainWindow = new MainWindow(new UserModel(user));
                            ErrorString = "";
                            mainWindow.Show();
                            Window.Close();
                        }
                        else
                        {
                            ErrorString = "Неправильный логин или пароль";
                        }

                    },
                    (obj) => Login != null && Password != null));
            }
        } 
        private string errorString = "";
        public string ErrorString
        {
            get => errorString;
            set
            {
                errorString = value;
                OnPropertyChanged("ErrorString");
            }
        } 




    }
}
