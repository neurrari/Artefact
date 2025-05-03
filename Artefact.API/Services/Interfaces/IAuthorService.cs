using Artefact.API.Data.Dtos.Author;

namespace Artefact.API.Services.Interfaces
{
    public interface IAuthorService : ICrudService<AuthorReadModel, AuthorCreateModel, AuthorUpdateModel> { }
}
