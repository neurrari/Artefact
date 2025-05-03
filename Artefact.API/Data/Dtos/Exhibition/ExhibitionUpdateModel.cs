using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Exhibition
{
    public class ExhibitionUpdateModel
    {
        [MaxLength(150)]
        public string? NameExhibition { get; set; }
        public DateTime? DateOpen { get; set; }
        public DateTime? DateClose { get; set; }
        public int? IdHall { get; set; }
    }
}
