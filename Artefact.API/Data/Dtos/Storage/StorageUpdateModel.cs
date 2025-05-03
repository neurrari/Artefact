using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Storage
{
    public class StorageUpdateModel
    {
        [MaxLength(13)]
        public string? NumberStorage { get; set; }
    }
}
