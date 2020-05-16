using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnector
{
    public static class RoleService
    {
        public static List<Role> GetRoles()
        {
            List<Role> roles;
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    roles = (from ro in db.Roles select ro).ToList(); 
                }
                return roles;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
