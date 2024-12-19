using Api.RequestModels;
using Api.ResponseModels;
using Api.Services.Interfaces;
using Azure.Core;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

namespace Api.Services.Implementations
{
    public class FounderService : IFounderService
    {
        private readonly IFounderRepository _founderRepository;
        private readonly IClientRepository _clientRepository;

        public FounderService(
            IFounderRepository founderRepository,
            IClientRepository clientRepository)
        {
            ArgumentNullException.ThrowIfNull(founderRepository);
            ArgumentNullException.ThrowIfNull(clientRepository);
            _founderRepository = founderRepository;
            _clientRepository = clientRepository;
        }

        public async Task Add(FounderAddRequest request, CancellationToken ct)
        {
            var newFounder = new Founder(request.Itn, request.Fullname);
            await _founderRepository.Add(newFounder, ct);
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            Founder founder;
            try
            {
                founder = await _founderRepository.GetById(id, ct);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Founder not found");
            }
            await _founderRepository.Delete(founder, ct);
        }

        public async Task<List<FounderResponse>> GetAll(CancellationToken ct)
        {
            var founders = await _founderRepository.GetAll(ct);
            if (founders.Count == 0)
                throw new NoContentException("Founders count is 0");
            return founders
                .Select(e => new FounderResponse(
                    e.Id,
                    e.Itn,
                    e.Fullname,
                    e.CreatedAt,
                    e.Client != null ? new ClientResponse(
                        e.Client.Id,
                        e.Client.Itn,
                        e.Client.Name,
                        e.Client.CreatedAt,
                        e.Client.Type,
                        []) : null))
                .ToList();
        }

        public async Task<FounderResponse> GetById(Guid id, CancellationToken ct)
        {
            Founder founder;
            try
            {
                founder = await _founderRepository.GetById(id, ct);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Founder not found");
            }
            return new FounderResponse(
                founder.Id,
                founder.Itn,
                founder.Fullname,
                founder.CreatedAt,
                founder.Client != null ? new ClientResponse(
                    founder.Client.Id,
                    founder.Client.Itn,
                    founder.Client.Name,
                    founder.Client.CreatedAt,
                    founder.Client.Type, []) : null);
        }

        public async Task<FounderResponse> Update(
            FounderUpdateRequest request,
            CancellationToken ct)
        {
            Founder founder;
            try
            {
                founder = await _founderRepository.GetById(request.Id, ct);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Founder not found");
            }
            if (request.Itn != null)
                founder.Itn = request.Itn.Value;
            if (request.Fullname != null)
                founder.Fullname = founder.Fullname;
            if (request.ClientId != null)
                try
                {
                    var client = await _clientRepository.GetById(request.ClientId.Value, ct);
                    founder.Client = client;
                }
                catch (InvalidOperationException)
                {
                    throw new NotFoundException("Client not found");
                }
            founder.UpdatedAt = DateTime.UtcNow;
            await _founderRepository.Update(founder, ct);
            return new FounderResponse(
                founder.Id,
                founder.Itn,
                founder.Fullname,
                founder.CreatedAt,
                founder.Client != null ? new ClientResponse(
                    founder.Client.Id,
                    founder.Client.Itn,
                    founder.Client.Name,
                    founder.Client.CreatedAt,
                    founder.Client.Type, []) : null);
        }
    }
}
