using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class ExhibitModel
    {
        public int Id { get; set; }
        public string RBnumber { get; set; }
        public int? IdInventoryCypher { get; set; }
        public string InventoryCypherName { get; set; }
        public int? InventoryNumber { get; set; }
        public string NameExhibit { get; set; }
        public int IdAuthor { get; set; }
        public string AuthorFullName { get; set; }
        public string PeriodYearCreated { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int? Height { get; set; }
        public int IdMeasure { get; set; }
        public string MeasureName { get; set; }
        public int IdTechnique { get; set; }
        public string TechniqueName { get; set; }
        public int? IdRecievingWay { get; set; }
        public string RecievingWayName { get; set; }
        public string CommonDescription { get; set; }
        public string DamageDescription { get; set; }
        public int IdStoringType { get; set; }
        public string StoringTypeName { get; set; }
        public byte[] Picture { get; set; }
        public DateTime DateRecordCreated { get; set; }
        public DateTime DateRecordLastUpdated { get; set; }
        public List<MaterialModel> Materials { get; set; } = new List<MaterialModel>();
    }
}