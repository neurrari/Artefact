using Artefact.API.Data.Dtos.Document;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentReadModel>>> GetDocuments()
        {
            return Ok(await _documentService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentReadModel>> GetDocument(int id)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null) return NotFound();
            return Ok(document);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentReadModel>> PostDocument(DocumentCreateModel dto)
        {
            try
            {
                var created = await _documentService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetDocument), new { id = created.Id }, created);
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
        public async Task<IActionResult> PutDocument(int id, DocumentUpdateModel dto)
        {
            try
            {
                var success = await _documentService.UpdateAsync(id, dto);
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
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                var success = await _documentService.DeleteAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }
    }
}
