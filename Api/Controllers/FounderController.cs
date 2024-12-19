using Api.RequestModels;
using Api.ResponseModels;
using Api.Services.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/founders")]
    public class FounderController : ControllerBase
    {
        private readonly IFounderService _founderService;

        public FounderController(IFounderService founderService)
        {
            ArgumentNullException.ThrowIfNull(founderService);
            _founderService = founderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FounderResponse>>> GetAll(
            CancellationToken ct)
        {
            try
            {
                return await _founderService.GetAll(ct);
            }
            catch (NoContentException)
            {
                return NoContent();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(
            [FromBody] FounderAddRequest request,
            CancellationToken ct)
        {
            await _founderService.Add(request, ct);
            return Created();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(
            [FromQuery] Guid founderId,
            CancellationToken ct)
        {
            try
            {
                await _founderService.Delete(founderId, ct);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FounderResponse>> GetById(
            Guid id,
            CancellationToken ct)
        {
            try
            {
                return await _founderService.GetById(id, ct);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(
            FounderUpdateRequest request,
            CancellationToken ct)
        {
            try
            {
                await _founderService.Update(request, ct);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok();
        }
    }
}
