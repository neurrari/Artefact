using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Employee
{
    public class EmployeeCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Firstname { get; set; } = null!;

        [MaxLength(30)]
        public string? Middlename { get; set; }

        [Required]
        [MaxLength(18)]
        public string Phone { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string Password { get; set; } = null!;

        [Required]
        public int RoleId { get; set; }
    }
}
