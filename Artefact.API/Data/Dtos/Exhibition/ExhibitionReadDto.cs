namespace Artefact.API.Data.Dtos.Exhibition
{
    public class ExhibitionReadDto
    {
        public int Id { get; set; }
        public string NameExhibition { get; set; } = null!;
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public int HallId { get; set; }
        public string? HallNumber { get; set; }
    }
}
