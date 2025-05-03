using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.StoringType
{
    public class StoringTypeUpdateModel
    {
        [MaxLength(50)]
        public string? NameStoringType { get; set; }
    }
}
