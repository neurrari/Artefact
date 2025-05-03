using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        [MaxLength(250)]
        [Column("Description")]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateDeadline { get; set; }
        [Required]
        public int IdStatus { get; set; }
        [ForeignKey("IdStatus")]
        public virtual Status Status { get; set; } = null!;
        [Required]
        public int IdCreator { get; set; }
        [ForeignKey("IdCreator")]
        public virtual Account CreatorAccount { get; set; } = null!;
        public int? IdAssigned { get; set; }
        [ForeignKey("IdAssigned")]
        public virtual Account? AssignedToAccount { get; set; }
    }
}