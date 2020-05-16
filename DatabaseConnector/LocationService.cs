using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnector
{
    public static class LocationService
    {
        public static List<Location> GetLocations()
        {
            List<Location> locations;
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    locations = (from locationdb in db.Locations select locationdb).Include(l => l.Country).Include(l => l.Conferences).ToList();
                }
                return locations;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public static bool AddLocation(Location location)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Locations.Add(location);
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
        
        public static void RemoveLocation(Location location)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Location l = (from lo in db.Locations where lo.LocationName == location.LocationName select lo).First();
                    db.Remove(l);
                    db.SaveChanges(); 
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ChangeLocation(Location locationChanged, string oldName)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Location l = (from lo in db.Locations where lo.LocationName == oldName select lo).First();
                    l.LocationName = locationChanged.LocationName;
                    l.LocationDescription = locationChanged.LocationDescription;
                    l.Country = locationChanged.Country;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AddConference(Location location, Conference conference)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Location l = (from lo in db.Locations where lo.LocationName == location.LocationName select lo).First();
                    Conference c = (from co in db.Conferences where co.ConferenceName == conference.ConferenceName select co).First();
                    l.Conferences.Add(c);
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
