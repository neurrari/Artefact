using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Role
{
    public class RoleCreateModel
    {
        [Required]
        [MaxLength(30)]
        public string NameRole { get; set; } = null!;
    }
}
