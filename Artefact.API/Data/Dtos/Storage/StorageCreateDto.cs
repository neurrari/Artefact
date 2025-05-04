using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Storage
{
    public class StorageCreateDto
    {
        [Required]
        [MaxLength(13)]
        public string NumberStorage { get; set; } = null!;
    }
}
