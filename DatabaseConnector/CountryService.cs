using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Linq;

namespace DatabaseConnector
{
    public static class CountryService
    {
        public  static List<Country> GetCountries()
        {
            List<Country> countries;
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    countries = (from countrydb in db.Countries select countrydb).ToList();

                }
                return countries;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public static bool AddCountry(Country country)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Countries.Add(country);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        //public static Country GetCountryByName(string name)
        //{
        //    Country country;
        //    try
        //    {
        //        using (ApplicationContext db = new ApplicationContext())
        //        {
        //            country = (from countrydb in db.Countries where countrydb.CountryName == name select countrydb).First();
        //        }
        //        return country;
        //    }
        //    catch(Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public static void RemoveCountry(Country country)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Country c = (from co in db.Countries where co.CountryName == country.CountryName select co).First();
                    db.Remove(c);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ChangeCountry(Country countryChanged, string oldName)
        {
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    Country c = (from co in db.Countries where co.CountryName == oldName select co).First();
                    c.CountryName = countryChanged.CountryName;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
