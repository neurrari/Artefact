namespace Artefact.API.Data.Dtos.Employee
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string? Middlename { get; set; }
        public string Phone { get; set; } = null!;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string FullName => $"{Lastname} {Firstname} {Middlename}".Trim(); // Calculated property
    }
}
