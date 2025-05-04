using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.StoringType
{
    public class StoringTypeUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string? NameStoringType { get; set; }
    }
}
