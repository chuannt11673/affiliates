using Affiliates.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Affiliates.Application
{
	public static class DependencyInjections
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddScoped<IUserService, UserService>();

			return services;
		}
	}
}
