using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnector
{
    public static class ReportService
    {
        public static List<Report> GetReports()
        {
            List<Report> reports;
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    reports = (from re in db.Reports select re).Include(re => re.Scientist).ToList();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return reports;
        }

        public static bool AddReport(Report report)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    db.Reports.Add(report);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public static void RemoveReport(Report report)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Report r = (from re in db.Reports where re.Id == report.Id select re).First();
                    db.Remove(r);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ChangeReport(Report report)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Report r = (from re in db.Reports where re.Id == report.Id select re).First();
                    r.IsPublished = report.IsPublished;
                    r.ReportDate = report.ReportDate;
                    r.ReportName = report.ReportName;
                    r.Text = report.Text;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
