using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string NameStatus { get; set; } = null!;
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}