using Api.RequestModels;
using Api.ResponseModels;
using Domain.Entities;

namespace Api.Services.Interfaces
{
    public interface IClientService
    {
        Task Add(ClientAddRequest request, CancellationToken ct);

        Task<List<ClientResponse>> GetAll(CancellationToken ct);

        Task<ClientResponse> GetById(Guid id, CancellationToken ct);

        Task<ClientResponse> Update(
            ClientUpdateRequest request, 
            CancellationToken ct);

        Task Delete(Guid id, CancellationToken ct);
    }
}
