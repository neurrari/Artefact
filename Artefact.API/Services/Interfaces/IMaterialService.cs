using Artefact.API.Data.Dtos.Material;

namespace Artefact.API.Services.Interfaces
{
    public interface IMaterialService : ICrudService<MaterialReadModel, MaterialCreateModel, MaterialUpdateModel> { }
}
