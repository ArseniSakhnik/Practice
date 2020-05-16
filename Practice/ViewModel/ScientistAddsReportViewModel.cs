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
    public class ScientistAddsReportViewModel : MVVMModel
    {
        public ObservableCollection<ReportModel> Reports { get; set; } = new ObservableCollection<ReportModel>();

        public ReportModel SelectedReport { get; set; }

        public ScientistModel SelectedScientist { get; set; }

        public ScientistAddsReportViewModel(ScientistModel selectedScientsist)
        {
            SelectedScientist = selectedScientsist;
            List<Report> reports = ReportService.GetReports();
            foreach (Report r in reports)
                Reports.Add(new ReportModel(r));
        }

        private RelayCommand addReportCommand;
        
        public RelayCommand AddReportCommand
        {
            get
            {
                return addReportCommand ??
                    (addReportCommand = new RelayCommand(obj =>
                    {
                        SelectedScientist.Reports.Add(SelectedReport);
                    },
                    (obj) => SelectedReport != null));
            }
        }

    }
}
