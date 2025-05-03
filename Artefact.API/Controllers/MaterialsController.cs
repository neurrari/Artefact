using Artefact.API.Data.Dtos.Material;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialReadModel>>> GetMaterials()
        {
            return Ok(await _materialService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialReadModel>> GetMaterial(int id)
        {
            var material = await _materialService.GetByIdAsync(id);
            if (material == null) return NotFound();
            return Ok(material);
        }

        [HttpPost]
        public async Task<ActionResult<MaterialReadModel>> PostMaterial(MaterialCreateModel dto)
        {
            try
            {
                var created = await _materialService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetMaterial), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial(int id, MaterialUpdateModel dto)
        {
            try
            {
                var success = await _materialService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            try
            {
                var success = await _materialService.DeleteAsync(id);
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
