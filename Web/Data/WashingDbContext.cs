using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data
{
	public class WashingDbContext : DbContext
	{
		public WashingDbContext(DbContextOptions<WashingDbContext> options) : base(options)
		{

		}

		public DbSet<Member> Members { get; set; }
		public DbSet<Clothing> Clothings { get; set; }
		public DbSet<ClothingType> ClothingTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Member>().ToTable("Member");
			modelBuilder.Entity<Clothing>().ToTable("Clothing");
			modelBuilder.Entity<Wid>().ToTable("Wid");
		}

		public DbSet<Web.Models.Wid> Wid { get; set; }

		public DbSet<Web.Models.Cst> Cst { get; set; }

		public DbSet<Web.Models.ClothingColor> ClothingColors { get; set; }

		public DbSet<Web.Models.ClothingStatus> ClothingStatus { get; set; }

		public DbSet<Web.Models.ClothingAction> ClothingActions { get; set; }

		public DbSet<Web.Models.ClothingPackageType> ClothingPackageTypes { get; set; }

		public DbSet<Web.Models.ClothingPicture> ClothingPictures { get; set; }

		public DbSet<Web.Models.Log> Logs { get; set; }
	}
}
