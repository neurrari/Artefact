using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Measure
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(4)]
        public string NameMeasure { get; set; } = null!;
        public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();
    }
}