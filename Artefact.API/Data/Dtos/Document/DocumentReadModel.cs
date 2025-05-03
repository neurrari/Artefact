namespace Artefact.API.Data.Dtos.Document
{
    public class DocumentReadModel
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public int IdDocumentType { get; set; }
        public string? DocumentTypeName { get; set; }
        public int? IdExhibition { get; set; }
        public string? ExhibitionName { get; set; }
        public List<ExhibitMinimalReadModel> Exhibits { get; set; } = new List<ExhibitMinimalReadModel>();
    }
}
