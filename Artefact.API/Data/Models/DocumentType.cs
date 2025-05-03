using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class DocumentType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string NameDocumentType { get; set; } = null!;
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}