using Artefact.API.Data.Dtos.Exhibit;

namespace Artefact.API.Services.Interfaces
{
    public interface IExhibitService
    {
        Task<IEnumerable<ExhibitReadModel>> GetAllAsync(int? storingTypeId = null);
        Task<ExhibitReadModel?> GetByIdAsync(int id);
        Task<ExhibitReadModel> CreateAsync(ExhibitCreateModel dto);
        Task<bool> UpdateAsync(int id, ExhibitUpdateModel dto);
        Task<bool> DeleteAsync(int id);
    }
}
