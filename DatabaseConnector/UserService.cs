using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnector
{
    public static class UserService
    {
        public static List<User> GetUsers()
        {
            List<User> users;
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    users = (from us in db.Users select us).Include(u => u.Role).ToList();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return users;
        }

        public static bool AddUser(User user)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Users.Add(user);
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

        public static void RemoveUser(User user)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User u = (from us in db.Users where us.Id == user.Id select us).First();
                    db.Remove(u);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ChangeUser(User user)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User u = (from us in db.Users where us.Id == user.Id select us).First();
                    u.UserName = user.UserName;
                    u.Login = user.Login;
                    u.Password = user.Password;
                    u.Role = user.Role;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static User GetUserByLoginAndPassword(string login, string password)
        {
            User user = null;
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    user = (from u in db.Users where u.Login == login && u.Password == password select u).Include(u => u.Role).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return user;
        }
    }
}
