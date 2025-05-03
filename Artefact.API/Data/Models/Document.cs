using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DocumentNumber { get; set; } = null!;
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public int IdDocumentType { get; set; }
        [ForeignKey("IdDocumentType")]
        public virtual DocumentType DocumentType { get; set; } = null!;
        public int? IdExhibition { get; set; }
        [ForeignKey("IdExhibition")]
        public virtual Exhibition? Exhibition { get; set; }
        public virtual List<ExhibitDocument> ExhibitDocuments { get; set; } = new List<ExhibitDocument>();
    }
}