using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.InventoryCypher
{
    public class InventoryCypherUpdateModel
    {
        [MaxLength(4)]
        public string? NameCypher { get; set; }
    }
}
