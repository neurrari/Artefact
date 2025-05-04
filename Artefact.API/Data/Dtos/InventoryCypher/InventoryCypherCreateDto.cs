using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.InventoryCypher
{
    public class InventoryCypherCreateDto
    {
        [Required]
        [MaxLength(4)]
        public string NameCypher { get; set; } = null!;
    }
}
