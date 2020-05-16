using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Models;
using DatabaseConnector;

namespace Practice.MVVMModels 
{
    public class ReportModel : MVVMModel
    {
        public Report Report { get; }

        public ReportModel(Report report) => Report = report;

        public string ReportName
        {
            get => Report.ReportName;
            set
            {
                Report.ReportName = value;
                if (value.Length > 0)
                    ReportService.ChangeReport(Report);
                OnPropertyChanged("ReportName");
            }
        }

        public string ScientistName
        {
            get => Report.Scientist.Name + " " + Report.Scientist.LastName;
            set { }
        }

        public string Text
        {
            get => Report.Text;
            set
            {
                Report.Text = value;
                if (value.Length > 0)
                    ReportService.ChangeReport(Report);
                OnPropertyChanged("Text");
            }
        }

        public DateTime ReportDate
        {
            get => Report.ReportDate;
            set
            {
                Report.ReportDate = value;
                ReportService.ChangeReport(Report);
                OnPropertyChanged("Report");
            }
        }

        public bool IsPublished
        {
            get => Report.IsPublished;
            set
            {
                Report.IsPublished = value;
                ReportService.ChangeReport(Report);
                OnPropertyChanged("Report");
            }
        }


    }
}
