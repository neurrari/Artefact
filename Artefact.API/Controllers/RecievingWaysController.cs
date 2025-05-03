using Artefact.API.Data.Dtos.RecievingWay;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecievingWaysController : ControllerBase
    {
        private readonly IRecievingWayService _recievingWayService;

        public RecievingWaysController(IRecievingWayService recievingWayService)
        {
            _recievingWayService = recievingWayService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecievingWayReadModel>>> GetRecievingWays()
        {
            return Ok(await _recievingWayService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecievingWayReadModel>> GetRecievingWay(int id)
        {
            var recievingWay = await _recievingWayService.GetByIdAsync(id);
            if (recievingWay == null) return NotFound();
            return Ok(recievingWay);
        }

        [HttpPost]
        public async Task<ActionResult<RecievingWayReadModel>> PostRecievingWay(RecievingWayCreateModel dto)
        {
            try
            {
                var created = await _recievingWayService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetRecievingWay), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecievingWay(int id, RecievingWayUpdateModel dto)
        {
            try
            {
                var success = await _recievingWayService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecievingWay(int id)
        {
            try
            {
                var success = await _recievingWayService.DeleteAsync(id);
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
