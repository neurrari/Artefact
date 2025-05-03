using Artefact.API.Data;
using Artefact.API.Data.Dtos.Caretaker;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class CaretakerService : ICaretakerService
    {
        private readonly BaseDbContext _context;

        public CaretakerService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<CaretakerReadModel> CreateAsync(CaretakerCreateModel dto)
        {
            var caretaker = new Caretaker
            {
                Lastname = dto.Lastname,
                Firstname = dto.Firstname,
                Middlename = dto.Middlename
            };
            _context.Caretakers.Add(caretaker);
            await _context.SaveChangesAsync();
            return new CaretakerReadModel
            {
                Id = caretaker.Id,
                Lastname = caretaker.Lastname,
                Firstname = caretaker.Firstname,
                Middlename = caretaker.Middlename
                // FullName is a calculated property in the DTO
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var caretaker = await _context.Caretakers.FindAsync(id);
            if (caretaker == null) return false;
            _context.Caretakers.Remove(caretaker);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<CaretakerReadModel>> GetAllAsync()
        {
            return await _context.Caretakers
                .Select(c => new CaretakerReadModel
                {
                    Id = c.Id,
                    Lastname = c.Lastname,
                    Firstname = c.Firstname,
                    Middlename = c.Middlename
                })
                .ToListAsync();
        }

        public async Task<CaretakerReadModel?> GetByIdAsync(int id)
        {
            var caretaker = await _context.Caretakers.FindAsync(id);
            return caretaker == null ? null : new CaretakerReadModel
            {
                Id = caretaker.Id,
                Lastname = caretaker.Lastname,
                Firstname = caretaker.Firstname,
                Middlename = caretaker.Middlename
            };
        }

        public async Task<bool> UpdateAsync(int id, CaretakerUpdateModel dto)
        {
            var caretaker = await _context.Caretakers.FindAsync(id);
            if (caretaker == null) return false;

            if (dto.Lastname != null) caretaker.Lastname = dto.Lastname;
            if (dto.Firstname != null) caretaker.Firstname = dto.Firstname;
            caretaker.Middlename = dto.Middlename; // Allows setting back to null

            _context.Entry(caretaker).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Caretakers.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}