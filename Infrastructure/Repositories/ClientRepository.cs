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
    public class ClientRepository : IClientRepository
    {
        private readonly SqlExpressDbContext _dbContext;

        private DbSet<Client> Clients => _dbContext.Set<Client>();

        public ClientRepository(SqlExpressDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Client entity, CancellationToken ct)
        {
            await Clients.AddAsync(entity, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task Delete(Client entity, CancellationToken ct)
        {
            Clients.Remove(entity);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<Client>> GetAll(CancellationToken ct)
        {
            return await Clients
                .Include(e => e.Founders)
                .ToListAsync(ct);
        }

        public Task<Client> GetById(Guid id, CancellationToken ct)
        {
            return Clients
                .Include(e => e.Founders)
                .FirstAsync(e => e.Id == id, ct);
        }

        public async Task Update(Client entity, CancellationToken ct)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
