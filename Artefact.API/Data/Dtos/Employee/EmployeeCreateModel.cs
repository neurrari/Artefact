using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Employee
{
    public class EmployeeCreateModel
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
        [MaxLength(15)]
        public string Phone { get; set; } = null!;
    }
}
