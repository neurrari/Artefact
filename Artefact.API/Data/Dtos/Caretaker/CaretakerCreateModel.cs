using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Caretaker
{
    public class CaretakerCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string Firstname { get; set; } = null!;
        [MaxLength(30)]
        public string? Middlename { get; set; }
    }
}
