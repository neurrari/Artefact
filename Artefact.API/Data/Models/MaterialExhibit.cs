using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Models
{
    public class MaterialExhibit
    {
        public int IdExhibit { get; set; }
        public virtual Exhibit Exhibit { get; set; } = null!;

        public int IdMaterial { get; set; }
        public virtual Material Material { get; set; } = null!;
    }
}