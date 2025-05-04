using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Hall
{
    public class HallUpdateDto
    {
        [MaxLength(8)]
        public string? NumberHall { get; set; }
        public int CaretakerId { get; set; }
    }
}
