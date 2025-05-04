using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Exhibit
{
    public class ExhibitCreateDto
    {
        [MaxLength(10)]
        public string? RBnumber { get; set; }
        public int? IdInventoryCypher { get; set; }
        public int InventoryNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string NameExhibit { get; set; } = null!;
        [Required]
        public int IdAuthor { get; set; }
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
        [Required]
        public int IdTechnique { get; set; }
        public int? IdRecievingWay { get; set; }
        public string? CommonDescription { get; set; }
        public string? DamageDescription { get; set; }
        [Required]
        public int IdStoringType { get; set; }
        public byte[]? Picture { get; set; }
        public List<int> MaterialIds { get; set; } = new List<int>();
        public List<DocumentMinimalReadDto> Documents { get; set; } = new();
    }
}
