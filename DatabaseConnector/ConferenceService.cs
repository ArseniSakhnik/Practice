using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnector
{
    public static class ConferenceService
    {
        public static List<Conference> GetConferences()
        {
            List<Conference> conferences;
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    conferences = (from c in db.Conferences select c).Include(c => c.ScientistConference).Include(c => c.Location).ThenInclude(l => l.Country).ToList();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return conferences;
        }

        public static bool AddConference(Conference conference)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    db.Conferences.Add(conference);
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

        public static void RemoveConference(Conference conference)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Conference con = (from c in db.Conferences where c.Id == conference.Id select c).First();
                    db.Remove(con);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ChangeConference(Conference conference)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Conference con = (from c in db.Conferences where c.Id == conference.Id select c).First();
                    con.ConferenceName = conference.ConferenceName;
                    con.ConferenceDescription = conference.ConferenceDescription;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Scientist> GetScientists(Conference conference)
        {
            List<Scientist> scientists = new List<Scientist>();
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Conference con = (from c in db.Conferences where c.Id == conference.Id select c).Include(c => c.ScientistConference).First();
                    foreach (ScientistConference sc in con.ScientistConference)
                    {
                        Scientist scientist = (from s in db.Scientists where s.Id == sc.ScientistId select s).First();
                        scientists.Add(scientist);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return scientists;
        }

        public static void AddScientist(Conference conference, Scientist scientist)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Conference con = (from c in db.Conferences where c.Id == conference.Id select c).First();
                    con.ScientistConference.Add(new ScientistConference { ConferenceId = conference.Id, ScientistId = scientist.Id });
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RemoveScientist(Conference conference, Scientist scientist)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Conference con = (from c in db.Conferences where c.Id == conference.Id select c).Include(c => c.ScientistConference).First();
                    ScientistConference sco = con.ScientistConference.First(sc => sc.ScientistId == scientist.Id);
                    con.ScientistConference.Remove(sco);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Conference> GetUnpublishedConferences()
        {
            List<Conference> unpublishedConferences = new List<Conference>();
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    List<Conference> conferences = (from c in db.Conferences select c).Include(c => c.ReportConference).ThenInclude(rc => rc.Report).ToList();
                    unpublishedConferences = (from c in conferences where c.ReportConference.All(rc => rc.Report.IsPublished == false) select c).ToList();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return unpublishedConferences;
        }

        public static List<Country> GetCountriesOnConference(Conference conference)
        {
            List<Country> countriesOnConference = new List<Country>();
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Conference con = (from c in db.Conferences where c.Id == conference.Id select c).Include(c => c.ScientistConference).First();
                    foreach(ScientistConference sc in con.ScientistConference)
                    {
                        Scientist scientist = (from s in db.Scientists where s.Id == sc.ScientistId select s).Include(s => s.Country).First();
                        if (countriesOnConference.All(coc => coc.CountryName == scientist.Country.CountryName))
                            countriesOnConference.Add(scientist.Country);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return countriesOnConference;
        }

        public static List<Report> GetReportsOnConference(Conference conference)
        {
            List<Report> reports = new List<Report>();
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Conference con = (from c in db.Conferences where c.Id == conference.Id select c).Include(c => c.ReportConference).First();
                    foreach (ReportConference rp in con.ReportConference)
                    {
                        Report report = (from r in db.Reports where r.Id == rp.ReportId select r).First();
                        reports.Add(report);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return reports;
        }

        public static void AddReport(Conference conference, Report report)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Conference con = (from c in db.Conferences where c.Id == conference.Id select c).Include(c => c.ReportConference).First();
                    con.ReportConference.Add(new ReportConference { ReportId = report.Id, ConferenceId = con.Id });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RemoveReport(Conference conference, Report report)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Conference con = (from c in db.Conferences where c.Id == conference.Id select c).Include(c => c.ReportConference).First();
                    ReportConference rc = con.ReportConference.First(rc => rc.ReportId == report.Id);
                    con.ReportConference.Remove(rc);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Report> GetReports(Conference conference)
        {
            List<Report> reports;
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    reports = (from r in db.Reports select r).Include(r => r.Scientist).ToList();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return reports;
        }

    }

}
