using Affiliates.Infratructure.Entities;
using Affiliates.Shared;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Affiliates.Infratructure
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext, IPersistedGrantDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Partner> Partners { get; set; }
		public DbSet<PersistedGrant> PersistedGrants { get; set; }
		public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<PersistedGrant>().HasNoKey();
			builder.Entity<DeviceFlowCodes>().HasNoKey();

			base.OnModelCreating(builder);
		}

		public async Task CommitAsync()
		{
			await SaveChangesAsync();
		}

		public async Task EnsureCreatedAsync()
		{
			Database.EnsureCreated();
			await Task.CompletedTask;
		}

		public async Task MigrateAsync()
		{
			Database.Migrate();
			await Task.CompletedTask;
		}

		public Task<int> SaveChangesAsync()
		{
			throw new System.NotImplementedException();
		}
	}
}
