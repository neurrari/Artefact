using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Material
{
    public class MaterialCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string NameMaterial { get; set; } = null!;
    }
}
