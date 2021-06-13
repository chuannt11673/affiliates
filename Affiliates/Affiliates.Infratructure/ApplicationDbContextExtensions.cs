using Affiliates.Infratructure.Repository;
using Affiliates.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using static IdentityModel.OidcConstants;

namespace Affiliates.Infratructure
{
	public static class ApplicationDbContextExtensions
	{
		public static IServiceCollection AddInfratructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

			services.AddIdentityServer()
				.AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options => {
					options.Clients.First(x => x.ClientId == "spa").AllowedGrantTypes = new List<string> { GrantTypes.Password };
				});

			services.AddAuthentication()
				.AddIdentityServerJwt();

			services.AddScoped<IDbContext, ApplicationDbContext>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			return services;
		}
	}
}
