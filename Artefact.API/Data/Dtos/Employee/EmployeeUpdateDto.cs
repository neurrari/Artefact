using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Employee
{
    public class EmployeeUpdateDto
    {
        [MaxLength(50)]
        public string? Lastname { get; set; }

        [MaxLength(30)]
        public string? Firstname { get; set; }

        [MaxLength(30)]
        public string? Middlename { get; set; }

        [MaxLength(18)]
        public string? Phone { get; set; }

        [MaxLength(128)]
        public string? Password { get; set; }

        public int? RoleId { get; set; }
    }
}