using System;
using System.Collections.Generic;
using System.Text;
using ClinicAppDAL.Models.ClinicModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ClinicAppDAL.EF
{
    public class ClinicAppClinicContext : DbContext
    {
        public DbSet<ClinicAppDAL.Models.ClinicModel.Diagnosis> Diagnoses { get; set; }
        public DbSet<ClinicAppDAL.Models.ClinicModel.Doctor> Doctors { get; set; }
        public DbSet<ClinicAppDAL.Models.ClinicModel.Patient> Patients { get; set; }
        public DbSet<ClinicAppDAL.Models.ClinicModel.DoctorVisit> DoctorVisits { get; set; }
        public ClinicAppClinicContext()
        {

        }
        public ClinicAppClinicContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = @"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\ClinicDB.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;";
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diagnosis>()
                .HasMany(e => e.DoctorVisits)
                .WithOne(e => e.Diagnosis);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasIndex(e => new { e.FirstName, e.MidName, e.LastName }).IsUnique();
            }).Entity<Doctor>()
                .HasMany(e => e.DoctorVisits)
                .WithOne(e => e.Doctor);

            modelBuilder.Entity<Patient>(entity => 
            {
                entity.HasIndex(e => new { e.FirstName, e.MidName, e.LastName }).IsUnique();
            }).Entity<Patient>()
                .HasMany(e => e.DoctorVisits)
                .WithOne(e=>e.Patient);
        }
    }
}
