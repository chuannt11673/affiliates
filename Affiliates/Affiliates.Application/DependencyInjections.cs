using Affiliates.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Affiliates.Application
{
	public static class DependencyInjections
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<IUserService, UserService>();
			return services;
		}
	}
}
