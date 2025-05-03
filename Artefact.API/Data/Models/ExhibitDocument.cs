using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artefact.API.Data.Models
{
    public class ExhibitDocument
    {
        public int IdExhibit { get; set; }
        public virtual Exhibit Exhibit { get; set; } = null!;

        public int IdDocument { get; set; }
        public virtual Document Document { get; set; } = null!;
    }
}