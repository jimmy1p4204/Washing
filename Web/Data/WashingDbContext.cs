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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Member>().ToTable("Member");
			modelBuilder.Entity<Clothing>().ToTable("Clothing");
			modelBuilder.Entity<Wid>().ToTable("Wid");
		}

		public DbSet<Web.Models.Wid> Wid { get; set; }

		public DbSet<Web.Models.Cst> Cst { get; set; }
	}
}
