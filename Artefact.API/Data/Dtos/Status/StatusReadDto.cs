using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Status
{
    public class StatusReadDto
    {
        public int Id { get; set; }
        public string NameStatus { get; set; } = null!;
    }
}
