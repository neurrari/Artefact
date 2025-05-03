using Artefact.API.Data;
using Artefact.API.Data.Dtos.InventoryCypher;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class InventoryCypherService : IInventoryCypherService
    {
        private readonly BaseDbContext _context;

        public InventoryCypherService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<InventoryCypherReadModel> CreateAsync(InventoryCypherCreateModel dto)
        {
            var cypher = new InventoryCypher { NameCypher = dto.NameCypher };
            _context.InventoryCyphers.Add(cypher);
            await _context.SaveChangesAsync();
            return new InventoryCypherReadModel { Id = cypher.Id, NameCypher = cypher.NameCypher };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cypher = await _context.InventoryCyphers.FindAsync(id);
            if (cypher == null) return false;
            _context.InventoryCyphers.Remove(cypher);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<InventoryCypherReadModel>> GetAllAsync()
        {
            return await _context.InventoryCyphers
                .Select(ic => new InventoryCypherReadModel { Id = ic.Id, NameCypher = ic.NameCypher })
                .ToListAsync();
        }

        public async Task<InventoryCypherReadModel?> GetByIdAsync(int id)
        {
            var cypher = await _context.InventoryCyphers.FindAsync(id);
            return cypher == null ? null : new InventoryCypherReadModel { Id = cypher.Id, NameCypher = cypher.NameCypher };
        }

        public async Task<bool> UpdateAsync(int id, InventoryCypherUpdateModel dto)
        {
            var cypher = await _context.InventoryCyphers.FindAsync(id);
            if (cypher == null) return false;

            if (dto.NameCypher != null)
            {
                cypher.NameCypher = dto.NameCypher;
            }
            _context.Entry(cypher).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.InventoryCyphers.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}