using System;
using System.Collections.Generic;
using System.Text;
using ClinicAppDAL.Models.AuthModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ClinicAppDAL.EF
{
    public class ClinicAppAuthContext : DbContext
    {
        public DbSet<ClinicAppDAL.Models.AuthModel.User> Users { get; set; }
        public ClinicAppAuthContext()
        {

        }
        public ClinicAppAuthContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = @"Server=tcp:kursdbserver.database.windows.net,1433;Initial Catalog=AuthDB;Persist Security Info=False;User ID=admin;Password=StrongPassword1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Login = "admin", Password = "admin", Role = 1 , Access = true
                });
        }
    }
}
