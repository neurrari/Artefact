using Artefact.API.Data.Dtos.Storage;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoragesController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public StoragesController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageReadModel>>> GetStorages()
        {
            return Ok(await _storageService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StorageReadModel>> GetStorage(int id)
        {
            var storage = await _storageService.GetByIdAsync(id);
            if (storage == null) return NotFound();
            return Ok(storage);
        }

        [HttpPost]
        public async Task<ActionResult<StorageReadModel>> PostStorage(StorageCreateModel dto)
        {
            try
            {
                var created = await _storageService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetStorage), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorage(int id, StorageUpdateModel dto)
        {
            try
            {
                var success = await _storageService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorage(int id)
        {
            try
            {
                var success = await _storageService.DeleteAsync(id);
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
