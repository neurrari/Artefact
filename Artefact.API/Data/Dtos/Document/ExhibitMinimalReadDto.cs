namespace Artefact.API.Data.Dtos.Document
{
    public class ExhibitMinimalReadDto
    {
        public int Id { get; set; }
        public string NameExhibit { get; set; } = null!;
        public string? RBnumber { get; set; }
        public int? InventoryNumber { get; set; }
    }
}
