using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artefact.API.Data.Models
{
    public class StorageCollection
    {
        public int IdCollection { get; set; }
        public virtual Collection Collection { get; set; } = null!;

        public int IdStorage { get; set; }
        public virtual Storage Storage { get; set; } = null!;
    }
}