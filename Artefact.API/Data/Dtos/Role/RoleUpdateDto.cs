using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Role
{
    public class RoleUpdateDto
    {
        [Required]
        [MaxLength(30)]
        public string? NameRole { get; set; }
    }
}
