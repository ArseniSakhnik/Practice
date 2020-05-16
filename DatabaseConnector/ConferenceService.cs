﻿using System;
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
                    conferences = (from c in db.Conferences select c).Include(c => c.Location).ToList();
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
    }
}
