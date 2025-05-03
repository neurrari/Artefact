using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Author
{
    public class AuthorCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string Firstname { get; set; } = null!;
        [MaxLength(30)]
        public string? Middlename { get; set; }
        [Required]
        public DateTime DateBirth { get; set; }
        public DateTime? DateDeath { get; set; }
    }
}
