using Artefact.API.Data.Dtos.Technique;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechniquesController : ControllerBase
    {
        private readonly ITechniqueService _techniqueService;

        public TechniquesController(ITechniqueService techniqueService)
        {
            _techniqueService = techniqueService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechniqueReadModel>>> GetTechniques()
        {
            return Ok(await _techniqueService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TechniqueReadModel>> GetTechnique(int id)
        {
            var technique = await _techniqueService.GetByIdAsync(id);
            if (technique == null) return NotFound();
            return Ok(technique);
        }

        [HttpPost]
        public async Task<ActionResult<TechniqueReadModel>> PostTechnique(TechniqueCreateModel dto)
        {
            try
            {
                var created = await _techniqueService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetTechnique), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechnique(int id, TechniqueUpdateModel dto)
        {
            try
            {
                var success = await _techniqueService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnique(int id)
        {
            try
            {
                var success = await _techniqueService.DeleteAsync(id);
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
