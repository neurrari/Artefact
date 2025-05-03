using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class InventoryCypher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(4)]
        public string NameCypher { get; set; } = null!;
        public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();
        public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
    }
}