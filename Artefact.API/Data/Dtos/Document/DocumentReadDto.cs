namespace Artefact.API.Data.Dtos.Document
{
    public class DocumentReadDto
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? DocumentTypeName { get; set; }
        public int? ExhibitionId { get; set; }
        public string? ExhibitionName { get; set; }
        public List<ExhibitMinimalReadDto> Exhibits { get; set; } = new();
    }
}
