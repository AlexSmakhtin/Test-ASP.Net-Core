using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SqlExpressRepository<TEntity> : 
        IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly SqlExpressDbContext _dbContext;

        private DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        public SqlExpressRepository(SqlExpressDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<TEntity> GetById(Guid id, CancellationToken ct)
        {
            return Entities.FirstAsync(e => e.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAll(CancellationToken ct)
        {
            return await Entities.ToListAsync(ct);
        }

        public async Task Add(TEntity entity, CancellationToken ct)
        {
            await Entities.AddAsync(entity, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task Update(TEntity entity, CancellationToken ct)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task Delete(TEntity entity, CancellationToken ct)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
