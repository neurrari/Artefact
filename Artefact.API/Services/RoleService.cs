using Artefact.API.Data;
using Artefact.API.Data.Dtos.Role;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly BaseDbContext _context;

        public RoleService(BaseDbContext context)
        {
            _context = context;
        }
        public async Task<RoleReadModel> CreateAsync(RoleCreateModel dto)
        {
            var role = new Role { NameRole = dto.NameRole };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return new RoleReadModel { Id = role.Id, NameRole = role.NameRole };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;
            _context.Roles.Remove(role);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<RoleReadModel>> GetAllAsync()
        {
            return await _context.Roles
                .Select(r => new RoleReadModel { Id = r.Id, NameRole = r.NameRole })
                .ToListAsync();
        }

        public async Task<RoleReadModel?> GetByIdAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            return role == null ? null : new RoleReadModel { Id = role.Id, NameRole = role.NameRole };
        }

        public async Task<bool> UpdateAsync(int id, RoleUpdateModel dto)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;

            if (dto.NameRole != null)
            {
                role.NameRole = dto.NameRole;
            }
            _context.Entry(role).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Roles.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}