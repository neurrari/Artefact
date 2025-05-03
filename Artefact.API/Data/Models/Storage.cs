using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Storage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(13)]
        public string NumberStorage { get; set; } = null!;
        public virtual ICollection<StorageCollection> StoragesCollections { get; set; } = new List<StorageCollection>();
    }
}