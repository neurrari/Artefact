using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.DocumentType
{
    public class DocumentTypeCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string NameDocumentType { get; set; } = null!;
    }
}
