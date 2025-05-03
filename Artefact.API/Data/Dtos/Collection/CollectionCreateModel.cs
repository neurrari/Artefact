using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Collection
{
    public class CollectionCreateModel
    {
        [Required]
        public int IdInventoryCypher { get; set; }
        [Required]
        [MaxLength(50)]
        public string NameCollection { get; set; } = null!;
        [Required]
        public int IdEmployee { get; set; }
        public List<int> StorageIds { get; set; } = new List<int>();
    }
}
