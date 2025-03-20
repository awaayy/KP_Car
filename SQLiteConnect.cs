using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIBDD.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace GIBDD
{
    public class SQLiteConnect : DbContext
    {
		public DbSet<User> Users { get; set; }
		public DbSet<Driver> Drivers { get; set; }
		public DbSet<DriverLicense> DriverLicenses { get; set; }
		public DbSet<EngineNumber> EngineNumbers { get; set; }
		public DbSet<VINNumber> VINNumbers { get; set; }

		public SQLiteConnect()
		{
			try
			{
			}
			catch
			{
				Database.EnsureCreated();
				return;
			}
			Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=GIBDD.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Driver>()
				.HasOne(d => d.DriverLicense)
				.WithOne(d => d.Driver)
				.HasForeignKey<DriverLicense>(d => d.DriverId);

			modelBuilder.Entity<User>()
				.HasOne(d => d.DriverLicense)
				.WithOne(d => d.User)
				.HasForeignKey<DriverLicense>(d => d.UserId);


			modelBuilder.Entity<EngineNumber>()
				.HasOne(e => e.VINNumber)
				.WithOne(v => v.EngineNumber)
				.HasForeignKey<VINNumber>(v => v.EngineNumberId);
		}


	}
}
