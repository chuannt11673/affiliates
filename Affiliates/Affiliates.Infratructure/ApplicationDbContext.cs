using Affiliates.Infratructure.Entities;
using Affiliates.Infratructure.Repository;
using Affiliates.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Affiliates.Infratructure
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Partner> Partners { get; set; }

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
	}

	public static class ApplicationDbContextExtensions
	{
		public static IServiceCollection AddInfratructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			services
				.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddScoped<IDbContext, ApplicationDbContext>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			return services;
		}
	}
}
