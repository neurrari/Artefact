using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string Firstname { get; set; } = null!;
        [MaxLength(30)]
        public string? Middlename { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime DateBirth { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateDeath { get; set; }
        public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();
    }
}