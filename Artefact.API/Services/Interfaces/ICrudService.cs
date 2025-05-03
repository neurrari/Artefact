namespace Artefact.API.Services.Interfaces
{
    public interface ICrudService<TReadDto, TCreateDto, TUpdateDto>
    {
        Task<IEnumerable<TReadDto>> GetAllAsync();
        Task<TReadDto?> GetByIdAsync(int id);
        Task<TReadDto> CreateAsync(TCreateDto dto);
        Task<bool> UpdateAsync(int id, TUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}