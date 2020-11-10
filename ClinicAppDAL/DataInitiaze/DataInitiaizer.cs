using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClinicAppDAL.DataInitiaze
{
    public class DataInitiaizer
    {
        public static void CreateDb(DbContext dbContext)
        {
            dbContext.Database.Migrate();
        }

        public static void InitialiseAdmin(DbContext context)
        {
            context.Add(new ClinicAppDAL.Models.AuthModel.User()
            {
                //Id = 1,
                Login = "admin",
                Password = "admin",
                Role = 1,
                Access = true
            });
            context.SaveChanges();
        }
    }
}
