namespace Artefact.API.Data.Dtos.Hall
{
    public class HallReadDto
    {
        public int Id { get; set; }
        public string NumberHall { get; set; } = null!;
        public int CaretakerId { get; set; }
        public string? CaretakerFullName { get; set; }
    }
}
