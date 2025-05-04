using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Status
{
    public class StatusUpdateDto
    {
        [Required]
        [MaxLength(30)]
        public string? NameStatus { get; set; }
    }
}
