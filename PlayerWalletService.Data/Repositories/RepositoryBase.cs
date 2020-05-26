using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlayerWalletService.Core.Exceptions;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerWalletService.Data.Repositories
{
    /// <summary>
    /// Repository base for repository pattern
    /// </summary>
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : ModelBase
    {

        /// <summary>
        /// Shared context instance.
        /// It comes from DI
        /// </summary>
        protected readonly PlayerWalletContext dataContext;

        /// <summary>
        /// logger instance
        /// It comes from DI
        /// </summary>
        protected readonly ILogger _logger;

        public RepositoryBase(PlayerWalletContext context,
                                 ILogger logger)
        {
            dataContext = context;
            _logger = logger;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>Returns a list of T</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await Set().AsNoTracking().ToListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Record Id</param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(int id)
        {
            var entity = await Set().FindAsync(id);

            if (entity == null)
                throw new RecordNotFoundException();

            return entity;
        }

        public virtual async Task AddAsync(T entity)
        {
            BeforeAdd(entity);

            Set().Add(entity);

            await SaveChangesAsync();

            AfterAdd(entity);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            BeforeDelete(entity);

            Set().Remove(entity);
            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetAsync(id);

                if (entity == null)
                    throw new RecordNotFoundException();

                await DeleteAsync(entity);
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            BeforeUpdate(entity);
            Set().Update(entity);
            await SaveChangesAsync();

            AfterUpdate(entity);
        }

        public IQueryable<T> Query() => Set().AsQueryable();

        public virtual void BeforeAdd(T entity)
        {
           entity.CreatedDate = new DateTime();
        }

        public virtual void BeforeUpdate(T entity)
        {
            entity.UpdatedDate = new DateTime();
        }

        public virtual void AfterAdd(T entity)
        {

        }

        public virtual void AfterUpdate(T entity)
        {

        }

        public virtual void BeforeDelete(T entity)
        {

        }

        public int ExecuteQuery(string sql, params object[] paramaters)
        {
            var result = dataContext.Database.ExecuteSqlCommand(sql, paramaters);
            dataContext.SaveChanges();

            return result;
        }

        private DbSet<T> Set() => dataContext.Set<T>();

        private Task SaveChangesAsync() => dataContext.SaveChangesAsync();
    }
}
