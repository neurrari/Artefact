using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Employee
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
        [MaxLength(15)]
        public string Phone { get; set; } = null!;
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
        public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
    }
}