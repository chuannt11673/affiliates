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
	public static class DependencyInjections
	{
		public static IServiceCollection AddInfratructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<ApplicationUser>()
				.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

			services.AddIdentityServer()
				.AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options => {
					var client = options.Clients.First(x => x.ClientId == "spa");
					client.AllowedGrantTypes = new List<string> { GrantTypes.Password };
					client.AllowOfflineAccess = true;
					client.RefreshTokenUsage = IdentityServer4.Models.TokenUsage.OneTimeOnly;
					client.AccessTokenLifetime = 60;
				});

			services.AddAuthentication()
			.AddIdentityServerJwt();

			services.AddScoped<IDbContext, ApplicationDbContext>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			services.AddHttpContextAccessor();

			return services;
		}
	}
}
