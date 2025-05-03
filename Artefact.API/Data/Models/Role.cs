using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string NameRole { get; set; } = null!;
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}