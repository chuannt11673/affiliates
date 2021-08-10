using Affiliates.Shared;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Affiliates.Infratructure
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IDbContext
	{
		public ApplicationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
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

		public async Task<int> SaveChangesAsync()
		{
			return await SaveChangesAsync();
		}
	}
}
