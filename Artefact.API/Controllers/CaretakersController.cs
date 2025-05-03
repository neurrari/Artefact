using Artefact.API.Data.Dtos.Caretaker;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaretakersController : ControllerBase
    {
        private readonly ICaretakerService _caretakerService;

        public CaretakersController(ICaretakerService caretakerService)
        {
            _caretakerService = caretakerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaretakerReadModel>>> GetCaretakers()
        {
            return Ok(await _caretakerService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CaretakerReadModel>> GetCaretaker(int id)
        {
            var caretaker = await _caretakerService.GetByIdAsync(id);
            if (caretaker == null) return NotFound();
            return Ok(caretaker);
        }

        [HttpPost]
        public async Task<ActionResult<CaretakerReadModel>> PostCaretaker(CaretakerCreateModel dto)
        {
            try
            {
                var created = await _caretakerService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetCaretaker), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaretaker(int id, CaretakerUpdateModel dto)
        {
            try
            {
                var success = await _caretakerService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaretaker(int id)
        {
            try
            {
                var success = await _caretakerService.DeleteAsync(id);
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
