using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.StoringType
{
    public class StoringTypeCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string NameStoringType { get; set; } = null!;
    }
}
