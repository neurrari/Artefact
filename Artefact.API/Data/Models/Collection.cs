using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IdInventoryCypher { get; set; }
        [ForeignKey("IdInventoryCypher")]
        public virtual InventoryCypher InventoryCypher { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string NameCollection { get; set; } = null!;
        [Required]
        public int IdEmployee { get; set; }
        [ForeignKey("IdEmployee")]
        public virtual Employee Employee { get; set; } = null!;
        public virtual List<StorageCollection> StoragesCollections { get; set; } = new List<StorageCollection>();
    }
}