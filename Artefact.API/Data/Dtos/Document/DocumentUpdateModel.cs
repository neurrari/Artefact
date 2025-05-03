using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Document
{
    public class DocumentUpdateModel
    {
        [MaxLength(10)]
        public string? DocumentNumber { get; set; }
        public int? IdDocumentType { get; set; }
        public int? IdExhibition { get; set; }
        public List<int>? ExhibitIds { get; set; }
    }
}
