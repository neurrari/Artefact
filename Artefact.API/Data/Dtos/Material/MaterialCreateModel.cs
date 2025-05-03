using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Material
{
    public class MaterialCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string NameMaterial { get; set; } = null!;
    }
}
