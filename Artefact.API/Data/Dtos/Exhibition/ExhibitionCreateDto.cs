using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Exhibition
{
    public class ExhibitionCreateDto
    {
        [Required]
        [MaxLength(150)]
        public string NameExhibition { get; set; } = null!;

        [Required]
        public DateTime DateOpen { get; set; }

        [Required]
        public DateTime DateClose { get; set; }

        public int? HallId { get; set; }
    }
}
