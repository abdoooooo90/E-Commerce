﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
	public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> 
	{
		Task<TEntity> GetAsync(TKey id);
		Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges);
		Task AddAsync(TEntity entity);
		void DeleteAsync(TEntity entity);
		void UpdateAync(TEntity entity);	
	}
}
