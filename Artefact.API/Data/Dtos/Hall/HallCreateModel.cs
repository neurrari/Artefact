using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Hall
{
    public class HallCreateModel
    {
        [Required]
        [MaxLength(8)]
        public string NumberHall { get; set; } = null!;
        [Required]
        public int IdCaretaker { get; set; }
    }
}
