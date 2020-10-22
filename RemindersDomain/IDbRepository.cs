using Microsoft.EntityFrameworkCore;
using RemindersDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindersDomain
{
    public interface IDbRepository<TEntity> where TEntity: class
    {
        Task<TEntity> Get(int id);
        Task<TEntity> Create(TEntity entity);
        Task Create(List<TEntity> entities);
        Task<TEntity> Update(TEntity entity);
        Task<int> Delete(int id);
        IQueryable<TEntity> Search();
    }

    public class DbRepository<TEntity> : IDbRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ReminderContext _context;

        public DbRepository(ReminderContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().AsNoTracking().Where(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Create(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Add(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            entity.LastUpdated = DateTime.UtcNow;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Set<TEntity>().Where(_ => _.Id == id).DeleteFromQueryAsync();
            return id;
        }

        public IQueryable<TEntity> Search()
        {
            return _context.Set<TEntity>().AsNoTracking().AsQueryable();
        }
    }
}
