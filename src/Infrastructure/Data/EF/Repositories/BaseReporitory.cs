using Domain.Abstractions;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.EF.Repositories
{
    public class BaseReporitory<TEntity> : IBaseRepository<TEntity> where TEntity : Entity<int>, IAggregateRoot
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _set;

        public BaseReporitory(DataContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _set.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _set.Where(expression).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _set.AsQueryable();
        }

        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _set.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _set.Remove(entity);
        }

        public void SoftDelete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _set.Update(entity);
        }
    }
}
