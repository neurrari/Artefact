using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class StoringType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string NameStoringType { get; set; } = null!;
        public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();
    }
}