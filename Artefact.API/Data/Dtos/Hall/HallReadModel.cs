namespace Artefact.API.Data.Dtos.Hall
{
    public class HallReadModel
    {
        public int Id { get; set; }
        public string NumberHall { get; set; } = null!;
        public int IdCaretaker { get; set; }
        public string? CaretakerFullName { get; set; }
    }
}
