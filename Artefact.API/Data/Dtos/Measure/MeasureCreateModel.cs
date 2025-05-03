using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Measure
{
    public class MeasureCreateModel
    {
        [Required]
        [MaxLength(4)]
        public string NameMeasure { get; set; } = null!;
    }
}
