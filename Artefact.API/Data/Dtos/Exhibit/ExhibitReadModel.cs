using Artefact.API.Data.Dtos.Material;

namespace Artefact.API.Data.Dtos.Exhibit
{
    public class ExhibitReadModel
    {
        public int Id { get; set; }
        public string? RBnumber { get; set; }
        public int? IdInventoryCypher { get; set; }
        public string? InventoryCypherName { get; set; }
        public int? InventoryNumber { get; set; }
        public string NameExhibit { get; set; } = null!;
        public int IdAuthor { get; set; }
        public string? AuthorFullName { get; set; }
        public string PeriodYearCreated { get; set; } = null!;
        public int Length { get; set; }
        public int Width { get; set; }
        public int? Height { get; set; }
        public int IdMeasure { get; set; }
        public string? MeasureName { get; set; }
        public int IdTechnique { get; set; }
        public string? TechniqueName { get; set; }
        public int? IdRecievingWay { get; set; }
        public string? RecievingWayName { get; set; }
        public string? CommonDescription { get; set; }
        public string? DamageDescription { get; set; }
        public int IdStoringType { get; set; }
        public string? StoringTypeName { get; set; }
        public byte[]? Picture { get; set; }
        public DateTime DateRecordCreated { get; set; }
        public DateTime DateRecordLastUpdated { get; set; }
        public List<MaterialReadModel> Materials { get; set; } = new List<MaterialReadModel>();
    }
}
