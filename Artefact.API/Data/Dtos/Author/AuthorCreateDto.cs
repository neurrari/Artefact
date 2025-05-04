using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Author
{
    public class AuthorCreateDto
    {
        [Required(AllowEmptyStrings = true)]
        public string Lastname { get; set; } = "Неизвестно"; 

        [Required(AllowEmptyStrings = true)]
        [MaxLength(30)]
        public string Firstname { get; set; } = "Неизвестно";

        [MaxLength(30)]
        public string? Middlename { get; set; }

        public DateTime? DateBirth { get; set; }

        public DateTime? DateDeath { get; set; }
    }
}
