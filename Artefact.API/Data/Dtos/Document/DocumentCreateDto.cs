using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Document
{
    public class DocumentCreateDto
    {
        [Required]
        [MaxLength(10)]
        public string DocumentNumber { get; set; } = null!;

        public int? DocumentTypeId { get; set; } 

        public int? ExhibitionId { get; set; }

        public List<int> ExhibitIds { get; set; } = new();
    }
}
