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
            var connectionString = @"Server=tcp:kursdbserver.database.windows.net,1433;Initial Catalog=AuthDB;Persist Security Info=False;User ID=admin;Password=StrongPassword1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            return new ClinicAppAuthContext(optionsBuilder.Options);
        }
    }
}
