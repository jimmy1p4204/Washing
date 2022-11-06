using Microsoft.EntityFrameworkCore;
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

		public DbSet<Wid> Wid { get; set; }

		public DbSet<Cst> Cst { get; set; }

		public DbSet<ClothingColor> ClothingColors { get; set; }

		public DbSet<ClothingStatus> ClothingStatus { get; set; }

		public DbSet<ClothingAction> ClothingActions { get; set; }

		public DbSet<ClothingPackageType> ClothingPackageTypes { get; set; }

		public DbSet<ClothingPicture> ClothingPictures { get; set; }

		public DbSet<Log> Logs { get; set; }

		public DbSet<MachineCash> MachineCashs { get; set; }
	}
}
