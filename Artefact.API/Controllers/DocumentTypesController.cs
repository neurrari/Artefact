using Artefact.API.Data.Dtos.DocumentType;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentTypesController : ControllerBase
    {
        private readonly IDocumentTypeService _documentTypeService;

        public DocumentTypesController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentTypeReadModel>>> GetDocumentTypes()
        {
            return Ok(await _documentTypeService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentTypeReadModel>> GetDocumentType(int id)
        {
            var documentType = await _documentTypeService.GetByIdAsync(id);
            if (documentType == null) return NotFound();
            return Ok(documentType);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentTypeReadModel>> PostDocumentType(DocumentTypeCreateModel dto)
        {
            try
            {
                var created = await _documentTypeService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetDocumentType), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentType(int id, DocumentTypeUpdateModel dto)
        {
            try
            {
                var success = await _documentTypeService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentType(int id)
        {
            try
            {
                var success = await _documentTypeService.DeleteAsync(id);
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
