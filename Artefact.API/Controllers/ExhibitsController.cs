using Artefact.API.Data.Dtos.Exhibit;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExhibitsController : ControllerBase
    {
        private readonly IExhibitService _exhibitService;

        public ExhibitsController(IExhibitService exhibitService)
        {
            _exhibitService = exhibitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExhibitReadModel>>> GetExhibits([FromQuery] int? storingTypeId = null)
        {
            return Ok(await _exhibitService.GetAllAsync(storingTypeId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExhibitReadModel>> GetExhibit(int id)
        {
            var exhibit = await _exhibitService.GetByIdAsync(id);
            if (exhibit == null) return NotFound();
            return Ok(exhibit);
        }

        [HttpPost]
        public async Task<ActionResult<ExhibitReadModel>> PostExhibit(ExhibitCreateModel dto)
        {
            try
            {
                var created = await _exhibitService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetExhibit), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExhibit(int id, ExhibitUpdateModel dto)
        {
            try
            {
                var success = await _exhibitService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibit(int id)
        {
            try
            {
                var success = await _exhibitService.DeleteAsync(id);
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
