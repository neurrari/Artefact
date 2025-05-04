using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Exhibit
{
    public class ExhibitUpdateDto
    {
        [MaxLength(10)]
        public string? RBnumber { get; set; }
        public int? InventoryCypherId { get; set; }
        public int? InventoryNumber { get; set; }
        [MaxLength(200)]
        public string? NameExhibit { get; set; }
        public int? AuthorId { get; set; }
        [MaxLength(100)]
        public string? PeriodYearCreated { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? MeasureId { get; set; }
        public int? TechniqueId { get; set; }
        public int? RecievingWayId { get; set; }
        public string? CommonDescription { get; set; }
        public string? DamageDescription { get; set; }
        public int? StoringTypeId { get; set; }
        public byte[]? Picture { get; set; } 
        public List<int>? MaterialIds { get; set; }
        public List<DocumentMinimalReadDto> Documents { get; set; } = new();
    }
}