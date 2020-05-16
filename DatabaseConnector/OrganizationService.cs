using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnector
{
    public static class OrganizationService
    {
        public static List<Organization> GetOrganizations()
        {
            List<Organization> organizations;
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    organizations = (from o in db.Organizations select o).Include(o => o.ScientistOrganization).ToList();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return organizations;
        }

        public static bool AddOrganization(Organization organization)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    db.Organizations.Add(organization);
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

        public static void RemoveOrganization(Organization organization)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Organization org = (from o in db.Organizations where o.Id == organization.Id select o).First();
                    db.Remove(org);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ChangeOrganization(Organization organization)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Organization org = (from o in db.Organizations where o.Id == organization.Id select o).First();
                    org.OrganizationName = organization.OrganizationName;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Scientist> GetScientists(Organization organization)
        {
            List<Scientist> scientists = new List<Scientist>();
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Organization org = (from or in db.Organizations where or.Id == organization.Id select or).Include(or => or.ScientistOrganization).First();
                    foreach (ScientistOrganization so in org.ScientistOrganization)
                    {
                        Scientist scientist = (from s in db.Scientists where s.Id == so.ScientistId select s).First();
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

        public static void AddScientist(Organization organization, Scientist scientist)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Organization org = (from o in db.Organizations where o.Id == organization.Id select o).First();
                    org.ScientistOrganization.Add(new ScientistOrganization { ScientistId = scientist.Id, OrganizationId = organization.Id });
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RemoveScientist(Organization organization, Scientist scientist)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Organization org = (from o in db.Organizations where o.Id == organization.Id select o).Include(o => o.ScientistOrganization).First();
                    ScientistOrganization so = org.ScientistOrganization.First(so => so.ScientistId == scientist.Id);
                    org.ScientistOrganization.Remove(so);
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
