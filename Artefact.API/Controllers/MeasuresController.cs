using Artefact.API.Data.Dtos.Measure;
using Artefact.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artefact.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasuresController : ControllerBase
    {
        private readonly IMeasureService _measureService;

        public MeasuresController(IMeasureService measureService)
        {
            _measureService = measureService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasureReadModel>>> GetMeasures()
        {
            return Ok(await _measureService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeasureReadModel>> GetMeasure(int id)
        {
            var measure = await _measureService.GetByIdAsync(id);
            if (measure == null) return NotFound();
            return Ok(measure);
        }

        [HttpPost]
        public async Task<ActionResult<MeasureReadModel>> PostMeasure(MeasureCreateModel dto)
        {
            try
            {
                var created = await _measureService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetMeasure), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasure(int id, MeasureUpdateModel dto)
        {
            try
            {
                var success = await _measureService.UpdateAsync(id, dto);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasure(int id)
        {
            try
            {
                var success = await _measureService.DeleteAsync(id);
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
