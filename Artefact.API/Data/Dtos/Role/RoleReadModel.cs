using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Role
{
    public class RoleReadModel
    {
        public int Id { get; set; }
        public string NameRole { get; set; } = null!;
    }
}
