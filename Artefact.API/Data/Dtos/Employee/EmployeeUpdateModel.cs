using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Employee
{
    public class EmployeeUpdateModel
    {
        [MaxLength(50)]
        public string? Lastname { get; set; }
        [MaxLength(30)]
        public string? Firstname { get; set; }
        [MaxLength(30)]
        public string? Middlename { get; set; }
        [MaxLength(15)]
        public string? Phone { get; set; }
    }
}