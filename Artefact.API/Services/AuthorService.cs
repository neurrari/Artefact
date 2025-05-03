using Artefact.API.Data;
using Artefact.API.Data.Dtos.Author;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly BaseDbContext _context;

        public AuthorService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorReadModel> CreateAsync(AuthorCreateModel dto)
        {
            var author = new Author
            {
                Lastname = dto.Lastname,
                Firstname = dto.Firstname,
                Middlename = dto.Middlename,
                DateBirth = dto.DateBirth,
                DateDeath = dto.DateDeath
            };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return new AuthorReadModel
            {
                Id = author.Id,
                Lastname = author.Lastname,
                Firstname = author.Firstname,
                Middlename = author.Middlename,
                DateBirth = author.DateBirth,
                DateDeath = author.DateDeath
                // FullName is calculated
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;
            _context.Authors.Remove(author);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<AuthorReadModel>> GetAllAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorReadModel
                {
                    Id = a.Id,
                    Lastname = a.Lastname,
                    Firstname = a.Firstname,
                    Middlename = a.Middlename,
                    DateBirth = a.DateBirth,
                    DateDeath = a.DateDeath
                }).ToListAsync();
        }

        public async Task<AuthorReadModel?> GetByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            return author == null ? null : new AuthorReadModel
            {
                Id = author.Id,
                Lastname = author.Lastname,
                Firstname = author.Firstname,
                Middlename = author.Middlename,
                DateBirth = author.DateBirth,
                DateDeath = author.DateDeath
            };
        }

        public async Task<bool> UpdateAsync(int id, AuthorUpdateModel dto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            if (dto.Lastname != null) author.Lastname = dto.Lastname;
            if (dto.Firstname != null) author.Firstname = dto.Firstname;
            if (dto.DateBirth.HasValue) author.DateBirth = dto.DateBirth.Value;

            author.Middlename = dto.Middlename; // Allows setting back to null
            author.DateDeath = dto.DateDeath; // Allows setting back to null or updating

            _context.Entry(author).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Authors.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}