namespace Artefact.API.Data.Dtos.Exhibition
{
    public class ExhibitionReadModel
    {
        public int Id { get; set; }
        public string NameExhibition { get; set; } = null!;
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public int IdHall { get; set; }
        public string? HallNumber { get; set; }
    }
}
