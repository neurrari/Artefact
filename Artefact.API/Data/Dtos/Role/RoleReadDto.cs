using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Role
{
    public class RoleReadDto
    {
        public int Id { get; set; }
        public string NameRole { get; set; } = null!;
    }
}
