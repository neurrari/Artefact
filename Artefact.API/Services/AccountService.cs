using Artefact.API.Data;
using Artefact.API.Data.Dtos.Account;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly BaseDbContext _context;

        public AccountService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<AccountReadModel> CreateAsync(AccountCreateModel dto)
        {
            // IMPORTANT: Hash the password before saving!
            // var hashedPassword = _passwordHasher.HashPassword(dto.Password);
            var hashedPassword = dto.Password; // Placeholder - DO NOT USE IN PRODUCTION

            var account = new Account
            {
                Login = dto.Login,
                Password = hashedPassword, // Store the hashed password
                DateCreated = DateTime.Now,
                IdEmployee = dto.IdEmployee,
                IdRole = dto.IdRole
            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var createdAccount = await _context.Accounts
                                    .Include(a => a.Employee)
                                    .Include(a => a.Role)
                                    .FirstOrDefaultAsync(a => a.Id == account.Id);

            return MapToReadModel(createdAccount);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;
            _context.Accounts.Remove(account);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<AccountReadModel>> GetAllAsync()
        {
            return await _context.Accounts
                   .Include(a => a.Employee)
                   .Include(a => a.Role)
                   .Select(a => MapToReadModel(a))
                   .ToListAsync();
        }

        public async Task<AccountReadModel?> GetByIdAsync(int id)
        {
            var account = await _context.Accounts
                           .Include(a => a.Employee)
                           .Include(a => a.Role)
                           .FirstOrDefaultAsync(a => a.Id == id);

            return account == null ? null : MapToReadModel(account);
        }

        public async Task<bool> UpdateAsync(int id, AccountUpdateModel dto)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;

            if (dto.Login != null) account.Login = dto.Login;
            if (dto.Password != null)
            {
                // IMPORTANT: Hash the new password!
                // account.Password = _passwordHasher.HashPassword(dto.Password);
                account.Password = dto.Password; // Placeholder - DO NOT USE IN PRODUCTION
            }
            if (dto.IdEmployee.HasValue) account.IdEmployee = dto.IdEmployee.Value;
            if (dto.IdRole.HasValue) account.IdRole = dto.IdRole.Value;

            _context.Entry(account).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Accounts.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }

        // Helper method for mapping Account to AccountReadModel
        private static AccountReadModel MapToReadModel(Account account)
        {
            if (account == null) return null;
            return new AccountReadModel
            {
                Id = account.Id,
                Login = account.Login,
                DateCreated = account.DateCreated,
                IdEmployee = account.IdEmployee,
                EmployeeFullName = account.Employee != null ? $"{account.Employee.Lastname} {account.Employee.Firstname} {account.Employee.Middlename ?? ""}".Trim() : null,
                IdRole = account.IdRole,
                RoleName = account.Role?.NameRole
            };
        }
        // Note: A separate AuthService would typically handle login logic
    }
}
