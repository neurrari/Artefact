using Artefact.API.Data.Dtos.Storage;

namespace Artefact.API.Services.Interfaces
{
    public interface IStorageService : ICrudService<StorageReadModel, StorageCreateModel, StorageUpdateModel> { }
}
