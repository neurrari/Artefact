namespace Artefact.API.Data.Dtos.Document
{
    public class ExhibitMinimalReadModel
    {
        public int Id { get; set; }
        public string NameExhibit { get; set; } = null!;
        public int InventoryNumber { get; set; }
    }
}
