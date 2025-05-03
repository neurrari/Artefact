using Artefact.API.Data;
using Artefact.API.Data.Dtos.Storage;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class StorageService : IStorageService
    {
        private readonly BaseDbContext _context;

        public StorageService(BaseDbContext context)
        {
            _context = context;
        }
        public async Task<StorageReadModel> CreateAsync(StorageCreateModel dto)
        {
            var storage = new Storage { NumberStorage = dto.NumberStorage };
            _context.Storages.Add(storage);
            await _context.SaveChangesAsync();
            return new StorageReadModel { Id = storage.Id, NumberStorage = storage.NumberStorage };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var storage = await _context.Storages.FindAsync(id);
            if (storage == null) return false;
            _context.Storages.Remove(storage);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<StorageReadModel>> GetAllAsync()
        {
            return await _context.Storages
               .Select(s => new StorageReadModel { Id = s.Id, NumberStorage = s.NumberStorage })
               .ToListAsync();
        }

        public async Task<StorageReadModel?> GetByIdAsync(int id)
        {
            var storage = await _context.Storages.FindAsync(id);
            return storage == null ? null : new StorageReadModel { Id = storage.Id, NumberStorage = storage.NumberStorage };
        }

        public async Task<bool> UpdateAsync(int id, StorageUpdateModel dto)
        {
            var storage = await _context.Storages.FindAsync(id);
            if (storage == null) return false;

            if (dto.NumberStorage != null)
            {
                storage.NumberStorage = dto.NumberStorage;
            }
            _context.Entry(storage).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Storages.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}