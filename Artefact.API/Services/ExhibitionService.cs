using Artefact.API.Data;
using Artefact.API.Data.Dtos.Exhibition;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly BaseDbContext _context;

        public ExhibitionService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<ExhibitionReadModel> CreateAsync(ExhibitionCreateModel dto)
        {
            var exhibition = new Exhibition
            {
                NameExhibition = dto.NameExhibition,
                DateOpen = dto.DateOpen,
                DateClose = dto.DateClose,
                IdHall = dto.IdHall
            };
            _context.Exhibitions.Add(exhibition);
            await _context.SaveChangesAsync();

            var createdExhibition = await _context.Exhibitions
                                   .Include(e => e.Hall)
                                   .FirstOrDefaultAsync(e => e.Id == exhibition.Id);

            return MapToReadModel(createdExhibition);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition == null) return false;
            _context.Exhibitions.Remove(exhibition);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ExhibitionReadModel>> GetAllAsync()
        {
            return await _context.Exhibitions
                .Include(e => e.Hall)
                .Select(e => MapToReadModel(e))
                .ToListAsync();
        }

        public async Task<ExhibitionReadModel?> GetByIdAsync(int id)
        {
            var exhibition = await _context.Exhibitions
                            .Include(e => e.Hall)
                            .FirstOrDefaultAsync(e => e.Id == id);

            return exhibition == null ? null : MapToReadModel(exhibition);
        }

        public async Task<bool> UpdateAsync(int id, ExhibitionUpdateModel dto)
        {
            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition == null) return false;

            if (dto.NameExhibition != null) exhibition.NameExhibition = dto.NameExhibition;
            if (dto.DateOpen.HasValue) exhibition.DateOpen = dto.DateOpen.Value;
            if (dto.DateClose.HasValue) exhibition.DateClose = dto.DateClose.Value;
            if (dto.IdHall.HasValue) exhibition.IdHall = dto.IdHall.Value;

            _context.Entry(exhibition).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Exhibitions.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }

        // Helper method for mapping Exhibition to ExhibitionReadModel
        private static ExhibitionReadModel MapToReadModel(Exhibition exhibition)
        {
            if (exhibition == null) return null;
            return new ExhibitionReadModel
            {
                Id = exhibition.Id,
                NameExhibition = exhibition.NameExhibition,
                DateOpen = exhibition.DateOpen,
                DateClose = exhibition.DateClose,
                IdHall = exhibition.IdHall,
                HallNumber = exhibition.Hall?.NumberHall
            };
        }
    }
}