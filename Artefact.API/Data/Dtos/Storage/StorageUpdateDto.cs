using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Storage
{
    public class StorageUpdateDto
    {
        [Required]
        [MaxLength(13)]
        public string? NumberStorage { get; set; }
    }
}
