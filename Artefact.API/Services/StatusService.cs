using Artefact.API.Data;
using Artefact.API.Data.Dtos.Status;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class StatusService : IStatusService
    {
        private readonly BaseDbContext _context;

        public StatusService(BaseDbContext context)
        {
            _context = context;
        }
        public async Task<StatusReadModel> CreateAsync(StatusCreateModel dto)
        {
            var status = new Status { NameStatus = dto.NameStatus };
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
            return new StatusReadModel { Id = status.Id, NameStatus = status.NameStatus };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null) return false;
            _context.Statuses.Remove(status);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<StatusReadModel>> GetAllAsync()
        {
            return await _context.Statuses
                .Select(s => new StatusReadModel { Id = s.Id, NameStatus = s.NameStatus })
                .ToListAsync();
        }

        public async Task<StatusReadModel?> GetByIdAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            return status == null ? null : new StatusReadModel { Id = status.Id, NameStatus = status.NameStatus };
        }

        public async Task<bool> UpdateAsync(int id, StatusUpdateModel dto)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null) return false;

            if (dto.NameStatus != null)
            {
                status.NameStatus = dto.NameStatus;
            }
            _context.Entry(status).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Statuses.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}