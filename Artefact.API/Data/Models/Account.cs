using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Artefact.API.Data.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [Column("Login")]
        public string Login { get; set; } = null!;
        [Required]
        [MaxLength(128)]
        [Column("Password")]
        public string Password { get; set; } = null!;
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public int IdEmployee { get; set; }
        [ForeignKey("IdEmployee")]
        public virtual Employee Employee { get; set; } = null!;
        [Required]
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public virtual Role Role { get; set; } = null!;


        [InverseProperty("CreatorAccount")]
        public virtual ICollection<Task> CreatedTasks { get; set; } = new List<Task>();
        [InverseProperty("AssignedToAccount")]
        public virtual ICollection<Task> AssignedTasks { get; set; } = new List<Task>();
    }
}