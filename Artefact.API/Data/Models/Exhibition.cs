using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Exhibition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string NameExhibition { get; set; } = null!;
        [Required]
        [Column(TypeName = "date")]
        public DateTime DateOpen { get; set; }
        [Required]
        [Column(TypeName = "date")] 
        public DateTime DateClose { get; set; }
        [Required]
        public int IdHall { get; set; }
        [ForeignKey("IdHall")]
        public virtual Hall Hall { get; set; } = null!;
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}