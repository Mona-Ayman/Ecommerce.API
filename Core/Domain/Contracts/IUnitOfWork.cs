﻿using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
        IGenericRepository<TEntity,TKey> GetRepository<TEntity,TKey>() where TEntity : BaseEntity<TKey>;
    }
}
