using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Collection
{
    public class CollectionUpdateModel
    {
        public int? IdInventoryCypher { get; set; }
        [MaxLength(50)]
        public string? NameCollection { get; set; }
        public int? IdEmployee { get; set; }
        public List<int>? StorageIds { get; set; }
    }
}
