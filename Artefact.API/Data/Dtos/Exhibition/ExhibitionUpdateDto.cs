using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Exhibition
{
    public class ExhibitionUpdateDto
    {
        [MaxLength(150)]
        public string? NameExhibition { get; set; }

        public DateTime? DateOpen { get; set; }

        public DateTime? DateClose { get; set; }

        public int HallId { get; set; }
    }
}
