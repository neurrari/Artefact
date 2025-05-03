using Artefact.API.Data;
using Artefact.API.Data.Dtos.Task;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly BaseDbContext _context;

        public TaskService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<TaskReadModel> CreateAsync(TaskCreateModel dto)
        {
            var task = new Data.Models.Task
            {
                Title = dto.Title,
                Description = dto.Description,
                DateCreated = DateTime.UtcNow,
                DateDeadline = dto.DateDeadline,
                IdStatus = dto.IdStatus,
                IdCreator = dto.IdCreator,
                IdAssigned = dto.IdAssigned
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var createdTask = await _context.Tasks
                                    .Include(t => t.Status)
                                    .Include(t => t.CreatorAccount).ThenInclude(a => a.Employee)
                                    .Include(t => t.AssignedToAccount).ThenInclude(a => a.Employee)
                                    .FirstOrDefaultAsync(t => t.Id == task.Id);

            return MapToReadModel(createdTask);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;
            _context.Tasks.Remove(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TaskReadModel>> GetAllAsync()
        {
            return await _context.Tasks
                    .Include(t => t.Status)
                    .Include(t => t.CreatorAccount).ThenInclude(a => a.Employee)
                    .Include(t => t.AssignedToAccount).ThenInclude(a => a.Employee)
                    .Select(t => MapToReadModel(t))
                    .ToListAsync();
        }

        public async Task<TaskReadModel?> GetByIdAsync(int id)
        {
            var task = await _context.Tasks
                            .Include(t => t.Status)
                            .Include(t => t.CreatorAccount).ThenInclude(a => a.Employee)
                            .Include(t => t.AssignedToAccount).ThenInclude(a => a.Employee)
                            .FirstOrDefaultAsync(t => t.Id == id);

            return task == null ? null : MapToReadModel(task);
        }

        public async Task<bool> UpdateAsync(int id, TaskUpdateModel dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            if (dto.Title != null) task.Title = dto.Title;
            if (dto.Description != null) task.Description = dto.Description;
            if (dto.DateDeadline.HasValue) task.DateDeadline = dto.DateDeadline.Value;
            if (dto.IdStatus.HasValue) task.IdStatus = dto.IdStatus.Value;
            task.IdAssigned = dto.IdAssigned; // Allows changing or setting to null

            _context.Entry(task).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tasks.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }

        // Helper method for mapping Task to TaskReadModel
        private static TaskReadModel MapToReadModel(Data.Models.Task task) // Use Models.Task
        {
            if (task == null) return null;
            return new TaskReadModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DateCreated = task.DateCreated,
                DateDeadline = task.DateDeadline,
                IdStatus = task.IdStatus,
                StatusName = task.Status?.NameStatus,
                IdCreator = task.IdCreator,
                CreatorFullName = task.CreatorAccount?.Employee != null ? $"{task.CreatorAccount.Employee.Lastname} {task.CreatorAccount.Employee.Firstname} {task.CreatorAccount.Employee.Middlename ?? ""}".Trim() : null,
                IdAssigned = task.IdAssigned,
                AssignedFullName = task.AssignedToAccount?.Employee != null ? $"{task.AssignedToAccount.Employee.Lastname} {task.AssignedToAccount.Employee.Firstname} {task.AssignedToAccount.Employee.Middlename ?? ""}".Trim() : null
            };
        }
    }
}