using Artefact.API.Data;
using Artefact.API.Data.Dtos.Measure;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly BaseDbContext _context;

        public MeasureService(BaseDbContext context)
        {
            _context = context;
        }
        public async Task<MeasureReadModel> CreateAsync(MeasureCreateModel dto)
        {
            var measure = new Measure { NameMeasure = dto.NameMeasure };
            _context.Measures.Add(measure);
            await _context.SaveChangesAsync();
            return new MeasureReadModel { Id = measure.Id, NameMeasure = measure.NameMeasure };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var measure = await _context.Measures.FindAsync(id);
            if (measure == null) return false;
            _context.Measures.Remove(measure);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MeasureReadModel>> GetAllAsync()
        {
            return await _context.Measures
                .Select(m => new MeasureReadModel { Id = m.Id, NameMeasure = m.NameMeasure })
                .ToListAsync();
        }

        public async Task<MeasureReadModel?> GetByIdAsync(int id)
        {
            var measure = await _context.Measures.FindAsync(id);
            return measure == null ? null : new MeasureReadModel { Id = measure.Id, NameMeasure = measure.NameMeasure };
        }

        public async Task<bool> UpdateAsync(int id, MeasureUpdateModel dto)
        {
            var measure = await _context.Measures.FindAsync(id);
            if (measure == null) return false;

            if (dto.NameMeasure != null)
            {
                measure.NameMeasure = dto.NameMeasure;
            }
            _context.Entry(measure).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Measures.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}