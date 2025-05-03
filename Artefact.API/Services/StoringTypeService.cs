using Artefact.API.Data;
using Artefact.API.Data.Dtos.StoringType;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class StoringTypeService : IStoringTypeService
    {
        private readonly BaseDbContext _context;

        public StoringTypeService(BaseDbContext context)
        {
            _context = context;
        }
        public async Task<StoringTypeReadModel> CreateAsync(StoringTypeCreateModel dto)
        {
            var storingType = new StoringType { NameStoringType = dto.NameStoringType };
            _context.StoringTypes.Add(storingType);
            await _context.SaveChangesAsync();
            return new StoringTypeReadModel { Id = storingType.Id, NameStoringType = storingType.NameStoringType };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var storingType = await _context.StoringTypes.FindAsync(id);
            if (storingType == null) return false;
            _context.StoringTypes.Remove(storingType);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<StoringTypeReadModel>> GetAllAsync()
        {
            return await _context.StoringTypes
                .Select(st => new StoringTypeReadModel { Id = st.Id, NameStoringType = st.NameStoringType })
                .ToListAsync();
        }

        public async Task<StoringTypeReadModel?> GetByIdAsync(int id)
        {
            var storingType = await _context.StoringTypes.FindAsync(id);
            return storingType == null ? null : new StoringTypeReadModel { Id = storingType.Id, NameStoringType = storingType.NameStoringType };
        }

        public async Task<bool> UpdateAsync(int id, StoringTypeUpdateModel dto)
        {
            var storingType = await _context.StoringTypes.FindAsync(id);
            if (storingType == null) return false;

            if (dto.NameStoringType != null)
            {
                storingType.NameStoringType = dto.NameStoringType;
            }
            _context.Entry(storingType).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.StoringTypes.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}