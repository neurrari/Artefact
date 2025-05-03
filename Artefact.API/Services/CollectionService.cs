using Artefact.API.Data;
using Artefact.API.Data.Dtos.Collection;
using Artefact.API.Data.Dtos.Storage;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly BaseDbContext _context;

        public CollectionService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<CollectionReadModel> CreateAsync(CollectionCreateModel dto)
        {
            var collection = new Collection
            {
                IdInventoryCypher = dto.IdInventoryCypher,
                NameCollection = dto.NameCollection,
                IdEmployee = dto.IdEmployee
            };

            // Handle Many-to-Many StorageCollections
            if (dto.StorageIds != null && dto.StorageIds.Any())
            {
                collection.StoragesCollections = dto.StorageIds.Select(storageId => new StorageCollection { IdStorage = storageId }).ToList();
            }

            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();

            var createdCollection = await _context.Collections
                                        .Include(c => c.InventoryCypher)
                                        .Include(c => c.Employee)
                                        .Include(c => c.StoragesCollections).ThenInclude(sc => sc.Storage)
                                        .FirstOrDefaultAsync(c => c.Id == collection.Id);

            return MapToReadModel(createdCollection);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null) return false;

            _context.Collections.Remove(collection);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<CollectionReadModel>> GetAllAsync()
        {
            return await _context.Collections
                    .Include(c => c.InventoryCypher)
                    .Include(c => c.Employee)
                    .Include(c => c.StoragesCollections).ThenInclude(sc => sc.Storage)
                    .Select(c => MapToReadModel(c))
                    .ToListAsync();
        }

        public async Task<CollectionReadModel?> GetByIdAsync(int id)
        {
            var collection = await _context.Collections
                                    .Include(c => c.InventoryCypher)
                                    .Include(c => c.Employee)
                                    .Include(c => c.StoragesCollections).ThenInclude(sc => sc.Storage)
                                    .FirstOrDefaultAsync(c => c.Id == id);

            return collection == null ? null : MapToReadModel(collection);
        }

        public async Task<bool> UpdateAsync(int id, CollectionUpdateModel dto)
        {
            var collection = await _context.Collections
                                    .Include(c => c.StoragesCollections)
                                    .FirstOrDefaultAsync(c => c.Id == id);

            if (collection == null) return false;

            if (dto.IdInventoryCypher.HasValue) collection.IdInventoryCypher = dto.IdInventoryCypher.Value;
            if (dto.NameCollection != null) collection.NameCollection = dto.NameCollection;
            if (dto.IdEmployee.HasValue) collection.IdEmployee = dto.IdEmployee.Value;

            // Handle Many-to-Many StorageCollections update
            if (dto.StorageIds != null)
            {
                collection.StoragesCollections.RemoveAll(sc => !dto.StorageIds.Contains(sc.IdStorage));

                var existingStorageIds = collection.StoragesCollections.Select(sc => sc.IdStorage).ToList();
                var newStorageIds = dto.StorageIds.Except(existingStorageIds);
                foreach (var storageId in newStorageIds)
                {
                    collection.StoragesCollections.Add(new StorageCollection { IdStorage = storageId });
                }
            }

            _context.Entry(collection).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Collections.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }

        // Helper method for mapping Collection to CollectionReadModel
        private static CollectionReadModel MapToReadModel(Collection collection)
        {
            if (collection == null) return null;
            return new CollectionReadModel
            {
                Id = collection.Id,
                IdInventoryCypher = collection.IdInventoryCypher,
                InventoryCypherName = collection.InventoryCypher?.NameCypher,
                NameCollection = collection.NameCollection,
                IdEmployee = collection.IdEmployee,
                EmployeeFullName = collection.Employee != null ? $"{collection.Employee.Lastname} {collection.Employee.Firstname} {collection.Employee.Middlename ?? ""}".Trim() : null,
                Storages = collection.StoragesCollections?
                            .Select(sc => sc.Storage == null ? null : new StorageReadModel { Id = sc.Storage.Id, NumberStorage = sc.Storage.NumberStorage })
                            .Where(s => s != null)
                            .ToList() ?? new List<StorageReadModel>()
            };
        }
    }
}