using Artefact.API.Data.Dtos.Collection;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionReadModel>>> GetCollections()
        {
            return Ok(await _collectionService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionReadModel>> GetCollection(int id)
        {
            var collection = await _collectionService.GetByIdAsync(id);
            if (collection == null) return NotFound();
            return Ok(collection);
        }

        [HttpPost]
        public async Task<ActionResult<CollectionReadModel>> PostCollection(CollectionCreateModel dto)
        {
            try
            {
                var created = await _collectionService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetCollection), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection(int id, CollectionUpdateModel dto)
        {
            try
            {
                var success = await _collectionService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            try
            {
                var success = await _collectionService.DeleteAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex) // Add more specific catches if needed
            {
                return StatusCode(500, "An error occurred.");
            }
        }
    }
}
