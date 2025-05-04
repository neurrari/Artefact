using Artefact.API.Data.Dtos.Material;

namespace Artefact.API.Data.Dtos.Exhibit
{
    public class ExhibitReadDto
    {
        public int Id { get; set; }
        public string? RBnumber { get; set; }
        public int? InventoryCypherId { get; set; }
        public string? InventoryCypherName { get; set; }
        public int? InventoryNumber { get; set; }
        public string NameExhibit { get; set; } = null!;
        public int? AuthorId { get; set; }
        public string? AuthorFullName { get; set; }
        public string PeriodYearCreated { get; set; } = null!;
        public int Length { get; set; }
        public int Width { get; set; }
        public int? Height { get; set; }
        public int? MeasureId { get; set; }
        public string? MeasureName { get; set; }
        public int? TechniqueId { get; set; }
        public string? TechniqueName { get; set; }
        public int? RecievingWayId { get; set; }
        public string? RecievingWayName { get; set; }
        public string? CommonDescription { get; set; }
        public string? DamageDescription { get; set; }
        public int? StoringTypeId { get; set; }
        public string? StoringTypeName { get; set; }
        public byte[]? Picture { get; set; } 
        public DateTime DateRecordCreated { get; set; }
        public DateTime DateRecordLastUpdated { get; set; }
        public List<MaterialReadDto> Materials { get; set; } = new();
        public List<DocumentMinimalReadDto> Documents { get; set; } = new();
    }
}
