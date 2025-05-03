using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.RecievingWay
{
    public class RecievingWayCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string NameWay { get; set; } = null!;
    }
}
