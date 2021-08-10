using System.Linq;
using System.Threading.Tasks;

namespace Affiliates.Shared
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<TEntity> FindAsync(params object[] ids);
		IQueryable<TEntity> Queryables { get; }
	}
}
