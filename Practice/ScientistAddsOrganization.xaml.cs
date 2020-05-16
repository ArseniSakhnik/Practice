using Models;
using Practice.MVVMModels;
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
    /// Логика взаимодействия для ScientistAddsOrganization.xaml
    /// </summary>
    public partial class ScientistAddsOrganization : Window
    {
        public ScientistAddsOrganization(ScientistModel selectedScientist)
        {
            InitializeComponent();
            DataContext = new ScientistAddsOrganizationViewModel(selectedScientist);
        }
    }
}
