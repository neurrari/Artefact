using Artefact.API.Data.Dtos.InventoryCypher;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryCyphersController : ControllerBase
    {
        private readonly IInventoryCypherService _inventoryCypherService;

        public InventoryCyphersController(IInventoryCypherService inventoryCypherService)
        {
            _inventoryCypherService = inventoryCypherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryCypherReadModel>>> GetInventoryCyphers()
        {
            return Ok(await _inventoryCypherService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryCypherReadModel>> GetInventoryCypher(int id)
        {
            var inventoryCypher = await _inventoryCypherService.GetByIdAsync(id);
            if (inventoryCypher == null) return NotFound();
            return Ok(inventoryCypher);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryCypherReadModel>> PostInventoryCypher(InventoryCypherCreateModel dto)
        {
            try
            {
                var created = await _inventoryCypherService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetInventoryCypher), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryCypher(int id, InventoryCypherUpdateModel dto)
        {
            try
            {
                var success = await _inventoryCypherService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryCypher(int id)
        {
            try
            {
                var success = await _inventoryCypherService.DeleteAsync(id);
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
