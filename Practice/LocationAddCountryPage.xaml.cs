using DatabaseConnector;
using Models;
using Practice.MVVMModels;
using Practice.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для LocationAddCountryPage.xaml
    /// </summary>
    public partial class LocationAddCountryPage : Window
    {
        public LocationAddCountryPage(LocationModel selectedModel)
        {
            InitializeComponent();
            this.DataContext = new LocationAddCountryPageViewModel(selectedModel, this);
        }

    }
}
