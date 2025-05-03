using Artefact.API.Data.Dtos.Hall;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HallsController : ControllerBase
    {
        private readonly IHallService _hallService;

        public HallsController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HallReadModel>>> GetHalls()
        {
            return Ok(await _hallService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HallReadModel>> GetHall(int id)
        {
            var hall = await _hallService.GetByIdAsync(id);
            if (hall == null) return NotFound();
            return Ok(hall);
        }

        [HttpPost]
        public async Task<ActionResult<HallReadModel>> PostHall(HallCreateModel dto)
        {
            try
            {
                var created = await _hallService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetHall), new { id = created.Id }, created);
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
        public async Task<IActionResult> PutHall(int id, HallUpdateModel dto)
        {
            try
            {
                var success = await _hallService.UpdateAsync(id, dto);
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
        public async Task<IActionResult> DeleteHall(int id)
        {
            try
            {
                var success = await _hallService.DeleteAsync(id);
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
