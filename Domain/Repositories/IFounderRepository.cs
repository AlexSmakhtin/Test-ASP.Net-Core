using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IFounderRepository : IRepository<Founder>
    {
        Task<IReadOnlyCollection<Founder>> GetRange(
            List<Guid> ids,
            CancellationToken ct);
    }
}
