﻿using System.Linq.Expressions;

namespace TaskManagementAPI.Repositories
{
    /// <summary>
    /// Базовый интерфейс репозитория.
    /// </summary>
    public interface IDBRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}