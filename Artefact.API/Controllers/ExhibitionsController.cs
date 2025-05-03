using Artefact.API.Data.Dtos.Exhibition;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExhibitionsController : ControllerBase
    {
        private readonly IExhibitionService _exhibitionService;

        public ExhibitionsController(IExhibitionService exhibitionService)
        {
            _exhibitionService = exhibitionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExhibitionReadModel>>> GetExhibitions()
        {
            return Ok(await _exhibitionService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExhibitionReadModel>> GetExhibition(int id)
        {
            var exhibition = await _exhibitionService.GetByIdAsync(id);
            if (exhibition == null) return NotFound();
            return Ok(exhibition);
        }

        [HttpPost]
        public async Task<ActionResult<ExhibitionReadModel>> PostExhibition(ExhibitionCreateModel dto)
        {
            try
            {
                var created = await _exhibitionService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetExhibition), new { id = created.Id }, created);
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
                return StatusCode(500, "Произошла ошибка.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExhibition(int id, ExhibitionUpdateModel dto)
        {
            try
            {
                var success = await _exhibitionService.UpdateAsync(id, dto);
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
                return StatusCode(500, "Произошла ошибка.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibition(int id)
        {
            try
            {
                var success = await _exhibitionService.DeleteAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка.");
            }
        }
    }
}