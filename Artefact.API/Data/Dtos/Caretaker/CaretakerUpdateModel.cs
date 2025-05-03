using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Caretaker
{
    public class CaretakerUpdateModel
    {
        [MaxLength(50)]
        public string? Lastname { get; set; }
        [MaxLength(30)]
        public string? Firstname { get; set; }
        [MaxLength(30)]
        public string? Middlename { get; set; }
    }
}
