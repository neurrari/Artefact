using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Exhibit
{
    public class DocumentMinimalReadDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string DocumentNumber { get; set; } = null!;
    }
}
