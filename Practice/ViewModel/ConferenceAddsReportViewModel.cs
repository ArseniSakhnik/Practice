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
    public class ConferenceAddsReportViewModel
    {

        public ObservableCollection<ReportModel> Reports { get; set; } = new ObservableCollection<ReportModel>();
        public Window Window { get; set; }
        public ReportModel SelectedReport { get; set; }
        public ConferenceModel SelectedConference { get; set; }

        public ConferenceAddsReportViewModel(Window window, ConferenceModel selectedConference)
        {
            Window = window;
            List <Report> reports = ConferenceService.GetReports(selectedConference.Conference);
            foreach (Report r in reports)
                Reports.Add(new ReportModel(r));
            SelectedConference = selectedConference;
        }

        private RelayCommand addReportCommand;

        public RelayCommand AddReportCommand
        {
            get
            {
                return addReportCommand ??
                    (addReportCommand = new RelayCommand(obj =>
                    {
                        SelectedConference.Reports.Add(SelectedReport);
                    },
                    (obj) => SelectedReport != null));
            }
        }
    }
}
