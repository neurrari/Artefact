using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Collection
{
    public class CollectionCreateDto
    {
        [Required]
        public int InventoryCypherId { get; set; }

        [Required]
        [MaxLength(50)]
        public string NameCollection { get; set; } = null!;

        [Required]
        public int EmployeeId { get; set; }

        public List<int> StorageIds { get; set; } = new(); 
    }
}
