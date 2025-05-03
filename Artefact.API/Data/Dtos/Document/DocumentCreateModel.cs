using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Document
{
    public class DocumentCreateModel
    {
        [Required]
        [MaxLength(10)]
        public string DocumentNumber { get; set; } = null!;

        // DateCreated handled by DB

        [Required]
        public int IdDocumentType { get; set; }
        public int? IdExhibition { get; set; }
        public List<int> ExhibitIds { get; set; } = new List<int>();
    }
}
