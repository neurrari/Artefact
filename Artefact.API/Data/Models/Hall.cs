using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(8)]
        public string NumberHall { get; set; } = null!;
        [Required]
        public int IdCaretaker { get; set; }
        [ForeignKey("IdCaretaker")]
        public virtual Caretaker Caretaker { get; set; } = null!;
        public virtual ICollection<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();
    }
}