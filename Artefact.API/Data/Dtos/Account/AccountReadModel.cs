namespace Artefact.API.Data.Dtos.Account
{
    public class AccountReadModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public int IdEmployee { get; set; }
        public string? EmployeeFullName { get; set; }
        public int IdRole { get; set; }
        public string? RoleName { get; set; }
    }
}