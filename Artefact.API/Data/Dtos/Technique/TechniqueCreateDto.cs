using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Technique
{
    public class TechniqueCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string NameTechnique { get; set; } = null!;
    }
}
