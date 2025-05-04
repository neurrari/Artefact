namespace Artefact.API.Data.Dtos.Task
{
    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateDeadline { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public int? CreatorId { get; set; }
        public string? CreatorFullName { get; set; }
        public int? AssignedId { get; set; }
        public string? AssignedFullName { get; set; }
    }
}
