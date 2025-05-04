using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Author
{
    public class AuthorUpdateDto
    {
        [MaxLength(50)]
        public string? Lastname { get; set; } 

        [MaxLength(30)]
        public string? Firstname { get; set; }

        [MaxLength(30)]
        public string? Middlename { get; set; }

        public DateTime? DateBirth { get; set; } 

        public DateTime? DateDeath { get; set; }
    }
}
