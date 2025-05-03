using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string NameMaterial { get; set; } = null!;
        public virtual ICollection<MaterialExhibit> MaterialExhibits { get; set; } = new List<MaterialExhibit>();
    }
}