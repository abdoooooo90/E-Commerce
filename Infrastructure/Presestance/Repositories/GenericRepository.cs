using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
	public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
	{
		private readonly StoreContext _storeContex;

		public GenericRepository(StoreContext storeContex)
        {
			_storeContex = storeContex;
		}
        public async Task AddAsync(TEntity entity)
		=> await _storeContex.Set<TEntity>().AddAsync(entity);

		public void DeleteAsync(TEntity entity)
		=> _storeContex.Set<TEntity>().Remove(entity);

		public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges)
			=> trackChanges ? await _storeContex.Set<TEntity>().ToListAsync()
			: await _storeContex.Set<TEntity>().AsNoTracking().ToListAsync();
		//{
		//	if(trackChanges)
		//		return await _storeContex.Set<TEntity>().ToListAsync();
		//	return await _storeContex.Set<TEntity>().AsNoTracking().ToListAsync();
		//}

		public async Task<TEntity> GetAsync(TKey id)
		=> await _storeContex.Set<TEntity>().FindAsync(id); 

		public void UpdateAync(TEntity entity)
		=> _storeContex.Set<TEntity>().Remove(entity);
	}
}
