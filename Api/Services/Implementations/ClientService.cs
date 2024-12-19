using Api.RequestModels;
using Api.ResponseModels;
using Api.Services.Interfaces;
using Azure.Core;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IFounderRepository _founderRepository;


        public ClientService(
            IClientRepository clientRepository,
            IFounderRepository founderRepository)
        {
            ArgumentNullException.ThrowIfNull(founderRepository);
            ArgumentNullException.ThrowIfNull(clientRepository);
            _clientRepository = clientRepository;
            _founderRepository = founderRepository;
        }

        public async Task Add(ClientAddRequest request, CancellationToken ct)
        {
            var founders = await _founderRepository.GetRange(request.FounderIds, ct);
            if (founders.Count == 0)
                throw new NotFoundException("Founders not found");
            var newClient = new Client(
                request.Itn,
                request.Name,
                request.Type)
            {
                Founders = [.. founders]
            };
            await _clientRepository.Add(newClient, ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            Client client;
            try
            {
                client = await _clientRepository.GetById(id, ct);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Client not found");
            }
            await _clientRepository.Delete(client, ct);
        }

        public async Task<List<ClientResponse>> GetAll(CancellationToken ct)
        {
            var clients = await _clientRepository.GetAll(ct);
            if (clients.Count == 0)
                throw new NoContentException("Clients count is 0");
            return clients
                .Select(e => new ClientResponse(
                    e.Id,
                    e.Itn,
                    e.Name,
                    e.CreatedAt,
                    e.Type,
                    e.Founders
                        .Select(e => new FounderResponse(
                            e.Id,
                            e.Itn,
                            e.Fullname,
                            e.CreatedAt))
                        .ToList()))
                .ToList();
        }

        public async Task<ClientResponse> GetById(Guid id, CancellationToken ct)
        {
            Client client;
            try
            {
                client = await _clientRepository.GetById(id, ct);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Client not found");
            }
            return new ClientResponse(
            client.Id,
            client.Itn,
            client.Name,
            client.CreatedAt,
            client.Type,
            client.Founders
                .Select(e => new FounderResponse(
                    e.Id,
                    e.Itn,
                    e.Fullname,
                    e.CreatedAt))
                .ToList());
        }

        public async Task<ClientResponse> Update(ClientUpdateRequest request, CancellationToken ct)
        {
            Client client;
            try
            {
                client = await _clientRepository.GetById(request.Id, ct);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Client not found");
            }
            if (request.Itn != null)
                client.Itn = request.Itn.Value;
            if (request.Name != null)
                client.Name = request.Name;
            if (request.Type != null)
                client.Type = request.Type.Value;
            if (request.FounderIds.Count != 0)
            {
                var founders = await _founderRepository.GetRange(request.FounderIds, ct);
                client.Founders = [.. founders];
            }
            client.UpdatedAt = DateTime.UtcNow;
            await _clientRepository.Update(client, ct);
            return new ClientResponse(
                client.Id,
                client.Itn,
                client.Name,
                client.CreatedAt,
                client.Type,
                client.Founders
                    .Select(e => new FounderResponse(
                        e.Id,
                        e.Itn,
                        e.Fullname,
                        e.CreatedAt))
                    .ToList());
        }
    }
}
