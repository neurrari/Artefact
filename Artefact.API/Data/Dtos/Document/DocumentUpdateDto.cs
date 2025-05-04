using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Document
{
    public class DocumentUpdateDto
    {
        [MaxLength(10)]
        public string? DocumentNumber { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? ExhibitionId { get; set; }
        public List<int>? ExhibitIds { get; set; }
    }
}
