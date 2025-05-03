using Artefact.API.Data;
using Artefact.API.Data.Dtos.RecievingWay;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class RecievingWayService : IRecievingWayService
    {
        private readonly BaseDbContext _context;

        public RecievingWayService(BaseDbContext context)
        {
            _context = context;
        }
        public async Task<RecievingWayReadModel> CreateAsync(RecievingWayCreateModel dto)
        {
            var recievingWay = new RecievingWay { NameWay = dto.NameWay };
            _context.RecievingWays.Add(recievingWay);
            await _context.SaveChangesAsync();
            return new RecievingWayReadModel { Id = recievingWay.Id, NameWay = recievingWay.NameWay };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recievingWay = await _context.RecievingWays.FindAsync(id);
            if (recievingWay == null) return false;
            _context.RecievingWays.Remove(recievingWay);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<RecievingWayReadModel>> GetAllAsync()
        {
            return await _context.RecievingWays
               .Select(rw => new RecievingWayReadModel { Id = rw.Id, NameWay = rw.NameWay })
               .ToListAsync();
        }

        public async Task<RecievingWayReadModel?> GetByIdAsync(int id)
        {
            var recievingWay = await _context.RecievingWays.FindAsync(id);
            return recievingWay == null ? null : new RecievingWayReadModel { Id = recievingWay.Id, NameWay = recievingWay.NameWay };
        }

        public async Task<bool> UpdateAsync(int id, RecievingWayUpdateModel dto)
        {
            var recievingWay = await _context.RecievingWays.FindAsync(id);
            if (recievingWay == null) return false;

            if (dto.NameWay != null)
            {
                recievingWay.NameWay = dto.NameWay;
            }
            _context.Entry(recievingWay).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.RecievingWays.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}