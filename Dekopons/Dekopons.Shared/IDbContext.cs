using System.Threading.Tasks;

namespace Affiliates.Shared
{
	public interface IDbContext
	{
		Task EnsureCreatedAsync();
		Task MigrateAsync();
		Task CommitAsync();
	}
}
