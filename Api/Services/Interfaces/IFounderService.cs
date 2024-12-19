using Api.RequestModels;
using Api.ResponseModels;
using Domain.Entities;

namespace Api.Services.Interfaces
{
    public interface IFounderService
    {
        Task Add(FounderAddRequest request, CancellationToken ct);

        Task<List<FounderResponse>> GetAll(CancellationToken ct);

        Task<FounderResponse> GetById(Guid id, CancellationToken ct);

        Task<FounderResponse> Update(
            FounderUpdateRequest request,
            CancellationToken ct);

        Task Delete(Guid id, CancellationToken ct);
    }
}
