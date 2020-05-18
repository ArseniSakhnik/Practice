using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Practice.MVVMModels;
using DatabaseConnector;
using System.Linq;
using Models;
using Practice.Commands;
using System.Collections;
using System.Windows.Controls;
namespace Practice.ViewModel
{
    public class ApplicationReportViewModel : MVVMModel
    {
        private ReportModel selectedReport;

        public ReportModel SelectedReport
        {
            get => selectedReport;
            set
            {
                selectedReport = value;
                OnPropertyChanged("SelectedReport");
            }
        }

        public ObservableCollection<ReportModel> Reports { get; set; } = new ObservableCollection<ReportModel>();

        private RelayCommand addReportCommand;

        public RelayCommand AddReportCommand
        {
            get
            {
                return addReportCommand ??
                    (addReportCommand = new RelayCommand(obj =>
                    {
                        Report report = new Report
                        {
                            ReportName = "Название доклада номер " + (Reports.Count + 1),
                            IsPublished = false
                        };

                        ReportModel rm = new ReportModel(report);
                        ReportService.AddReport(report);
                        Reports.Insert(0, rm);
                        selectedReport = rm;
                    },
                    (obj) =>
                    {
                        if (selectedReport != null)
                        {
                            IEnumerable<ReportModel> a = (from c in Reports where c.ReportName.Length == 0 select c);
                            if (a.Count() > 0)
                            {
                                return false;
                            }
                            else
                                return true;
                        }
                        return true;
                    }));
            }
        }

        private RelayCommand removeReportCommand;

        public RelayCommand RemoveReportCommand
        {
            get
            {
                return removeReportCommand ??
                    (removeReportCommand = new RelayCommand(obj =>
                    {
                        ReportModel cm = obj as ReportModel;
                        if (cm != null)
                            Reports.Remove(cm);
                    },
                    (obj) => selectedReport != null));
            }
        }

        private RelayCommand findReportCommand;

        public RelayCommand FindReportCommand
        {
            get
            {
                return findReportCommand ??
                    (findReportCommand = new RelayCommand(obj =>
                    {
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            Reports = new ObservableCollection<ReportModel>(Reports.OrderByDescending(c => c.ReportName.Contains(obj.ToString())));
                            OnPropertyChanged("Reports");
                        }
                        else if (obj != null)
                        {
                            Reports = new ObservableCollection<ReportModel>(Reports.OrderBy(c => c.ReportName));
                            OnPropertyChanged("Reports");
                        }
                    }));
            }
        }


        private RelayCommand datePickerCommand;
        public RelayCommand DatePickerCommand
        {
            get
            {
                return datePickerCommand ??
                    (datePickerCommand = new RelayCommand(obj =>
                    {
                        DatePicker datePicker = new DatePicker(selectedReport);
                        datePicker.Show();
                    },
                    (obj) => selectedReport != null));
            }
        }

        public ApplicationReportViewModel()
        {
            List<Report> reports = ReportService.GetReports();
            foreach (Report c in reports)
                Reports.Add(new ReportModel(c));

            Reports.CollectionChanged += (o, e) =>
            {
                if (e.Action.ToString().Equals("Add"))
                {
                    ReportModel reportModel = null;
                    foreach (ReportModel rm in e.NewItems)
                        reportModel = rm;

                    ReportService.AddReport(reportModel.Report);
                }
                else if (e.Action.ToString().Equals("Remove"))
                {
                    ReportModel reportModel = null;
                    foreach (ReportModel rm in e.OldItems)
                        reportModel = rm;

                    ReportService.RemoveReport(reportModel.Report);
                }
                OnPropertyChanged("Reports");
            };
        }
    
        
    }
}
