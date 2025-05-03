using Artefact.API.Data.Dtos.Task;

namespace Artefact.API.Services.Interfaces
{
    public interface ITaskService : ICrudService<TaskReadModel, TaskCreateModel, TaskUpdateModel> { }
}
