using Practice.ViewModel;
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

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            DataContext = new AuthorizationViewModel(this);
        }

        //public void WriteMessage(string message)
        //{
        //    Label label = new Label();
        //    label.Name = "Error";
        //    label.Content = message;
        //    label.Margin = new Thickness(20, 182, 20, 75);
        //    label.Foreground = Brushes.Red; 
        //    this.AddChild(label);
        //}

        //public void DeleteMessage()
        //{
        //    this.Child
        //}

    }
}
