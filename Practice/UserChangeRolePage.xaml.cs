using Practice.MVVMModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Practice.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для UserChangeRolePage.xaml
    /// </summary>
    public partial class UserChangeRolePage : Window
    {
        public UserChangeRolePage(UserModel selectedUser)
        {
            InitializeComponent();
            this.DataContext = new UserChangeRoleViewModel(selectedUser, this);
        }
    }
}
