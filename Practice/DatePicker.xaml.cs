using Models;
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
    /// Логика взаимодействия для DatePicker.xaml
    /// </summary>
    public partial class DatePicker : Window
    {
        private ReportModel SelectedReport { get; set; }
        public DatePicker(ReportModel selectedReport)
        {
            InitializeComponent();
            SelectedReport = selectedReport;
        }

        private void calendar1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = calendar1.SelectedDate;
            SelectedReport.ReportDate = (DateTime)selectedDate;
            SelectedReport.OnPropertyChanged("ReportDate");
            this.Close();
        }

    }
}
