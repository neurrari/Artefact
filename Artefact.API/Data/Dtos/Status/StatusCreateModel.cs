using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Status
{
    public class StatusCreateModel
    {
        [Required]
        [MaxLength(30)]
        public string NameStatus { get; set; } = null!;
    }
}
