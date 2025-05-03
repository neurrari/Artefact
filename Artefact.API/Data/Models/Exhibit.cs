using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Exhibit
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string? RBnumber { get; set; }
        public int? IdInventoryCypher { get; set; }
        [ForeignKey("IdInventoryCypher")]
        public virtual InventoryCypher? InventoryCypher { get; set; }
        public int InventoryNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string NameExhibit { get; set; } = null!;
        [Required]
        public int IdAuthor { get; set; }
        [ForeignKey("IdAuthor")]
        public virtual Author Author { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string PeriodYearCreated { get; set; } = null!;
        [Required]
        public int Length { get; set; }
        [Required]
        public int Width { get; set; }
        public int? Height { get; set; }
        [Required]
        public int IdMeasure { get; set; }
        [ForeignKey("IdMeasure")]
        public virtual Measure Measure { get; set; } = null!;
        [Required]
        public int IdTechnique { get; set; }
        [ForeignKey("IdTechnique")]
        public virtual Technique Technique { get; set; } = null!;
        public int? IdRecievingWay { get; set; }
        [ForeignKey("IdRecievingWay")]
        public virtual RecievingWay? RecievingWay { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string? CommonDescription { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string? DamageDescription { get; set; }
        [Required]
        public int IdStoringType { get; set; }
        [ForeignKey("IdStoringType")]
        public virtual StoringType StoringType { get; set; } = null!;
        public byte[]? Picture { get; set; }
        [Required]
        public DateTime DateRecordCreated { get; set; }
        [Required]
        public DateTime DateRecordLastUpdated { get; set; }


        public virtual List<MaterialExhibit> MaterialExhibits { get; set; } = new List<MaterialExhibit>();
        public virtual List<ExhibitDocument> ExhibitDocuments { get; set; } = new List<ExhibitDocument>();
    }
}