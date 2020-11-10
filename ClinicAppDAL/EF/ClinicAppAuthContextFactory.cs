using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ClinicAppDAL.EF
{
    public class ClinicAppAuthContextFactory : IDesignTimeDbContextFactory<ClinicAppAuthContext>
    {
        public ClinicAppAuthContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClinicAppAuthContext>();
            var connectionString = @"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\AuthDB.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;";
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            return new ClinicAppAuthContext(optionsBuilder.Options);
        }
    }
}
