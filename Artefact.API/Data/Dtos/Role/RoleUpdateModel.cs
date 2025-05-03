using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Role
{
    public class RoleUpdateModel
    {
        [MaxLength(30)]
        public string? NameRole { get; set; }
    }
}
