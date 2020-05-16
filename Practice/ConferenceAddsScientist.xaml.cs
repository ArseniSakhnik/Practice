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
using Practice.ViewModel;

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для ConferenceAddsScientist.xaml
    /// </summary>
    public partial class ConferenceAddsScientist : Window
    {
        public ConferenceAddsScientist(ConferenceModel selectedConferece)
        {
            InitializeComponent();
            this.DataContext = new ConferenceAddsScientistViewModel(this, selectedConferece);
        }
    }
}
