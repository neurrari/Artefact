using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Collection
{
    public class CollectionUpdateDto
    {
        public int? InventoryCypherId { get; set; }

        [MaxLength(50)]
        public string? NameCollection { get; set; }

        public int? EmployeeId { get; set; }

        public List<int>? StorageIds { get; set; }
    }
}
