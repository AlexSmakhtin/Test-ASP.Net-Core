using Api.RequestModels;
using Api.ResponseModels;
using Api.Services.Implementations;
using Api.Services.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            ArgumentNullException.ThrowIfNull(clientService);
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientResponse>>> GetAll(
            CancellationToken ct)
        {
            try
            {
                return await _clientService.GetAll(ct);
            }
            catch (NoContentException)
            {
                return NoContent();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(
            [FromBody] ClientAddRequest request,
            CancellationToken ct)
        {
            try
            {
                await _clientService.Add(request, ct);
            }
            catch (NotFoundException ex)
            {
                NotFound(ex.Message);
            }
            return Created();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(
            [FromQuery] Guid clientId,
            CancellationToken ct)
        {
            try
            {
                await _clientService.Delete(clientId, ct);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClientResponse>> GetById(
            Guid id,
            CancellationToken ct)
        {
            try
            {
                return await _clientService.GetById(id, ct);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(
            ClientUpdateRequest request,
            CancellationToken ct)
        {
            try
            {
                await _clientService.Update(request, ct);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok();
        }
    }
}
