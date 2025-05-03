using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Hall
{
    public class HallUpdateModel
    {
        [MaxLength(8)]
        public string? NumberHall { get; set; }
        public int? IdCaretaker { get; set; }
    }
}
