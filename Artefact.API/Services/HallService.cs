using Artefact.API.Data;
using Artefact.API.Data.Dtos.Hall;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class HallService : IHallService
    {
        private readonly BaseDbContext _context;

        public HallService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<HallReadModel> CreateAsync(HallCreateModel dto)
        {
            var hall = new Hall
            {
                NumberHall = dto.NumberHall,
                IdCaretaker = dto.IdCaretaker
            };
            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();

            // Need to load caretaker name for the read model
            var createdHall = await _context.Halls
                                    .Include(h => h.Caretaker)
                                    .FirstOrDefaultAsync(h => h.Id == hall.Id);

            return MapToReadModel(createdHall);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null) return false;
            _context.Halls.Remove(hall);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<HallReadModel>> GetAllAsync()
        {
            return await _context.Halls
                    .Include(h => h.Caretaker)
                    .Select(h => MapToReadModel(h))
                    .ToListAsync();
        }

        public async Task<HallReadModel?> GetByIdAsync(int id)
        {
            var hall = await _context.Halls
                            .Include(h => h.Caretaker)
                            .FirstOrDefaultAsync(h => h.Id == id);

            return hall == null ? null : MapToReadModel(hall);
        }

        public async Task<bool> UpdateAsync(int id, HallUpdateModel dto)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null) return false;

            if (dto.NumberHall != null) hall.NumberHall = dto.NumberHall;
            if (dto.IdCaretaker.HasValue) hall.IdCaretaker = dto.IdCaretaker.Value;

            _context.Entry(hall).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Halls.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }

        // Helper method for mapping Hall to HallReadModel
        private static HallReadModel MapToReadModel(Hall hall)
        {
            if (hall == null) return null;
            return new HallReadModel
            {
                Id = hall.Id,
                NumberHall = hall.NumberHall,
                IdCaretaker = hall.IdCaretaker,
                CaretakerFullName = hall.Caretaker != null ? $"{hall.Caretaker.Lastname} {hall.Caretaker.Firstname} {hall.Caretaker.Middlename ?? ""}".Trim() : null
            };
        }
    }
}