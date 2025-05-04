using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Technique
{
    public class TechniqueUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string NameTechnique { get; set; } = null!;
    }
}
