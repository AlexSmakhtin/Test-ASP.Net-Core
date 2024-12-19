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
    public class FounderRepository : IFounderRepository
    {
        private readonly SqlExpressDbContext _dbContext;

        private DbSet<Founder> Founders => _dbContext.Set<Founder>();

        public FounderRepository(SqlExpressDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Founder entity, CancellationToken ct)
        {
            await Founders.AddAsync(entity, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task Delete(Founder entity, CancellationToken ct)
        {
            Founders.Remove(entity);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<Founder>> GetAll(CancellationToken ct)
        {
            return await Founders.ToListAsync(ct);
        }

        public Task<Founder> GetById(Guid id, CancellationToken ct)
        {
            return Founders
                .Include(e => e.Client)
                .FirstAsync(e => e.Id == id, ct);
        }

        public async Task Update(Founder entity, CancellationToken ct)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<Founder>> GetRange(
            List<Guid> ids,
            CancellationToken ct)
        {
            return await Founders
                .Include(e => e.Client)
                .Where(e => ids.Contains(e.Id))
                .ToListAsync(ct);
        }
    }
}
