using Artefact.API.Data;
using Artefact.API.Data.Dtos.Employee;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly BaseDbContext _context;

        public EmployeeService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeReadModel> CreateAsync(EmployeeCreateModel dto)
        {
            var employee = new Employee
            {
                Lastname = dto.Lastname,
                Firstname = dto.Firstname,
                Middlename = dto.Middlename,
                Phone = dto.Phone
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return new EmployeeReadModel
            {
                Id = employee.Id,
                Lastname = employee.Lastname,
                Firstname = employee.Firstname,
                Middlename = employee.Middlename,
                Phone = employee.Phone
                // FullName is calculated
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;
            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<EmployeeReadModel>> GetAllAsync()
        {
            return await _context.Employees
               .Select(e => new EmployeeReadModel
               {
                   Id = e.Id,
                   Lastname = e.Lastname,
                   Firstname = e.Firstname,
                   Middlename = e.Middlename,
                   Phone = e.Phone
               })
               .ToListAsync();
        }

        public async Task<EmployeeReadModel?> GetByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee == null ? null : new EmployeeReadModel
            {
                Id = employee.Id,
                Lastname = employee.Lastname,
                Firstname = employee.Firstname,
                Middlename = employee.Middlename,
                Phone = employee.Phone
            };
        }

        public async Task<bool> UpdateAsync(int id, EmployeeUpdateModel dto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            if (dto.Lastname != null) employee.Lastname = dto.Lastname;
            if (dto.Firstname != null) employee.Firstname = dto.Firstname;
            if (dto.Phone != null) employee.Phone = dto.Phone;
            employee.Middlename = dto.Middlename; // Allows setting back to null

            _context.Entry(employee).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Employees.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}
