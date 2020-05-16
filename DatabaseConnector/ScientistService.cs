using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnector
{
    public static class ScientistService
    {
        public static List<Scientist> GetScientists()
        {
            List<Scientist> scientists;
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    scientists = (from s in db.Scientists select s).Include(s => s.Reports).ToList();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return scientists;
        }

        public static bool AddScientist(Scientist scientist)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    db.Scientists.Add(scientist);
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

        public static void RemoveScientist(Scientist scientist)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Scientist sci = (from s in db.Scientists where s.Id == scientist.Id select s).First();
                    db.Remove(sci);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ChangeScientist(Scientist scientist)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Scientist sci = (from s in db.Scientists where s.Id == scientist.Id select s).First();
                    sci.Name = scientist.Name;
                    sci.LastName = scientist.LastName;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AddReport(Scientist scientist, Report report)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Scientist sci = (from s in db.Scientists where s.Id == scientist.Id select s).First();
                    Report rep = (from r in db.Reports where r.Id == report.Id select r).First();
                    sci.Reports.Add(rep);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RemoveReport(Scientist scientist, Report report)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Scientist sci = (from s in db.Scientists where s.Id == scientist.Id select s).First();
                    Report rep = (from r in db.Reports where r.Id == report.Id select r).First();
                    sci.Reports.Remove(rep);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Conference> GetConferences(Scientist scientist)
        {
            List<Conference> conferences = new List<Conference>();
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Scientist sci = (from s in db.Scientists where s.Id == scientist.Id select s).Include(s => s.ScientistConference).First();
                    foreach(ScientistConference sc in sci.ScientistConference)
                    {
                        Conference conference = (from c in db.Conferences where c.Id == sc.ConferenceId select c).First();
                        conferences.Add(conference);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return conferences;
        }

        public static void AddConference(Scientist scientist, Conference conference)
        {
            ConferenceService.AddScientist(conference, scientist);
        }

        public static void RemoveConference(Scientist scientist, Conference conference)
        {
            ConferenceService.RemoveScientist(conference, scientist);
        }

        public static List<Organization> GetOrganizations(Scientist scientist)
        {
            List<Organization> organizations = new List<Organization>();
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Scientist sci = (from s in db.Scientists where s.Id == scientist.Id select s).Include(s => s.ScientistOrganization).First();
                    foreach(ScientistOrganization or in sci.ScientistOrganization)
                    {
                        Organization organization = (from o in db.Organizations where o.Id == or.OrganizationId select o).First();
                        organizations.Add(organization);
                    }    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return organizations;
        }

        public static void AddOrganization(Scientist scientist, Organization organization)
        {
            OrganizationService.AddScientist(organization, scientist);
        }

        public static void RemoveOrganization(Scientist scientist, Organization organization)
        {
            OrganizationService.RemoveScientist(organization, scientist);
        }

    }
}
