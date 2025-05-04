using Artefact.API.Data.Dtos.Storage;

namespace Artefact.API.Data.Dtos.Collection
{
    public class CollectionReadDto
    {
        public int Id { get; set; }
        public int InventoryCypherId { get; set; }
        public string? InventoryCypherName { get; set; }
        public string NameCollection { get; set; } = null!;
        public int EmployeeId { get; set; }
        public string? EmployeeFullName { get; set; }
        public List<StorageReadDto> Storages { get; set; } = new();
    }
}
