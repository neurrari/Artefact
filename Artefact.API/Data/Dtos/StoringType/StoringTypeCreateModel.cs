using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.StoringType
{
    public class StoringTypeCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string NameStoringType { get; set; } = null!;
    }
}
