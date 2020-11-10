using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ClinicAppDAL.EF
{
    public class ClinicAppClinicContextFactory : IDesignTimeDbContextFactory<ClinicAppClinicContext>
    {
        public ClinicAppClinicContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClinicAppClinicContext>();
            var connectionString = @"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\ClinicDB.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;";
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            return new ClinicAppClinicContext(optionsBuilder.Options);
        }
    }
}
