using Artefact.API.Data.Dtos.Status;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusesController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusReadModel>>> GetStatuses()
        {
            return Ok(await _statusService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusReadModel>> GetStatus(int id)
        {
            var status = await _statusService.GetByIdAsync(id);
            if (status == null) return NotFound();
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<StatusReadModel>> PostStatus(StatusCreateModel dto)
        {
            try
            {
                var created = await _statusService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetStatus), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, StatusUpdateModel dto)
        {
            try
            {
                var success = await _statusService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            try
            {
                var success = await _statusService.DeleteAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }
    }
}
