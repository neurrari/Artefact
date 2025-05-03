using Artefact.API.Data.Dtos.Employee;

namespace Artefact.API.Services.Interfaces
{
    public interface IEmployeeService : ICrudService<EmployeeReadModel, EmployeeCreateModel, EmployeeUpdateModel> { }
}
