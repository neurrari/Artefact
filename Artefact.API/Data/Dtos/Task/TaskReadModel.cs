namespace Artefact.API.Data.Dtos.Task
{
    public class TaskReadModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateDeadline { get; set; }
        public int IdStatus { get; set; }
        public string? StatusName { get; set; }
        public int IdCreator { get; set; }
        public string? CreatorFullName { get; set; }
        public int? IdAssigned { get; set; }
        public string? AssignedFullName { get; set; }
    }
}
