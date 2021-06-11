using Affiliates.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Affiliates.Infratructure.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly DbSet<TEntity> _dbSet;

		public Repository(ApplicationDbContext dbContext)
		{
			_dbSet = dbContext.Set<TEntity>();
		}

		public IQueryable<TEntity> Queryables => _dbSet;

		public async Task<TEntity> FindAsync(params object[] ids)
		{
			var entity = await _dbSet.FindAsync(ids);
			return entity;
		}
	}
}
