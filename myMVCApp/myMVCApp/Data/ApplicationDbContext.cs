using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myMVCApp.Models;

namespace myMVCApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Tree> Trees { get; set; }

        public DbSet<Sensor> Sensors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to Many (One locations can have many Sensors)
            modelBuilder.Entity<Sensor>()
                .HasOne(l => l.Location)
                .WithMany(s => s.Sensors)
                .HasForeignKey(l => l.LocationId);

            //One to Many (One locations can have many Trees)
            modelBuilder.Entity<Tree>()
                .HasOne(t => t.Location)
                .WithMany(t => t.Trees)
                .HasForeignKey(l => l.LocationId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
