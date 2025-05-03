using Artefact.API.Data.Dtos.Storage;

namespace Artefact.API.Data.Dtos.Collection
{
    public class CollectionReadModel
    {
        public int Id { get; set; }
        public int IdInventoryCypher { get; set; }
        public string? InventoryCypherName { get; set; }
        public string NameCollection { get; set; } = null!;
        public int IdEmployee { get; set; }
        public string? EmployeeFullName { get; set; }
        public List<StorageReadModel> Storages { get; set; } = new List<StorageReadModel>();
    }
}
