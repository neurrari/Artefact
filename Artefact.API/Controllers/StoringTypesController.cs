using Artefact.API.Data.Dtos.StoringType;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoringTypesController : ControllerBase
    {
        private readonly IStoringTypeService _storingTypeService;

        public StoringTypesController(IStoringTypeService storingTypeService)
        {
            _storingTypeService = storingTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoringTypeReadModel>>> GetStoringTypes()
        {
            return Ok(await _storingTypeService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoringTypeReadModel>> GetStoringType(int id)
        {
            var storingType = await _storingTypeService.GetByIdAsync(id);
            if (storingType == null) return NotFound();
            return Ok(storingType);
        }

        [HttpPost]
        public async Task<ActionResult<StoringTypeReadModel>> PostStoringType(StoringTypeCreateModel dto)
        {
            try
            {
                var created = await _storingTypeService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetStoringType), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoringType(int id, StoringTypeUpdateModel dto)
        {
            try
            {
                var success = await _storingTypeService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoringType(int id)
        {
            try
            {
                var success = await _storingTypeService.DeleteAsync(id);
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
