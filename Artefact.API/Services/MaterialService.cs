using Artefact.API.Data;
using Artefact.API.Data.Dtos.Material;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly BaseDbContext _context;

        public MaterialService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<MaterialReadModel> CreateAsync(MaterialCreateModel dto)
        {
            var material = new Material { NameMaterial = dto.NameMaterial };
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();
            return new MaterialReadModel { Id = material.Id, NameMaterial = material.NameMaterial };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return false;
            _context.Materials.Remove(material);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MaterialReadModel>> GetAllAsync()
        {
            return await _context.Materials
                .Select(m => new MaterialReadModel { Id = m.Id, NameMaterial = m.NameMaterial })
                .ToListAsync();
        }

        public async Task<MaterialReadModel?> GetByIdAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            return material == null ? null : new MaterialReadModel { Id = material.Id, NameMaterial = material.NameMaterial };
        }

        public async Task<bool> UpdateAsync(int id, MaterialUpdateModel dto)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return false;

            if (dto.NameMaterial != null)
            {
                material.NameMaterial = dto.NameMaterial;
            }
            _context.Entry(material).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Materials.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}