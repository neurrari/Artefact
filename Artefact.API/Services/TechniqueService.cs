using Artefact.API.Data;
using Artefact.API.Data.Dtos.Technique;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class TechniqueService : ITechniqueService
    {
        private readonly BaseDbContext _context;

        public TechniqueService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<TechniqueReadModel> CreateAsync(TechniqueCreateModel dto)
        {
            var technique = new Technique
            {
                NameTechnique = dto.NameTechnique
            };
            _context.Techniques.Add(technique);
            await _context.SaveChangesAsync();
            return new TechniqueReadModel
            {
                Id = technique.Id,
                NameTechnique = technique.NameTechnique
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var technique = await _context.Techniques.FindAsync(id);
            if (technique == null) return false;
            _context.Techniques.Remove(technique);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TechniqueReadModel>> GetAllAsync()
        {
            return await _context.Techniques
                .Select(t => new TechniqueReadModel
                {
                    Id = t.Id,
                    NameTechnique = t.NameTechnique
                })
                .ToListAsync();
        }

        public async Task<TechniqueReadModel?> GetByIdAsync(int id)
        {
            var technique = await _context.Techniques.FindAsync(id);
            return technique == null ? null : new TechniqueReadModel
            {
                Id = technique.Id,
                NameTechnique = technique.NameTechnique
            };
        }

        public async Task<bool> UpdateAsync(int id, TechniqueUpdateModel dto)
        {
            var technique = await _context.Techniques.FindAsync(id);
            if (technique == null) return false;

            if (dto.NameTechnique != null)
            {
                technique.NameTechnique = dto.NameTechnique;
            }
            _context.Entry(technique).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Techniques.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}