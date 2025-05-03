using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Status
{
    public class StatusReadModel
    {
        public int Id { get; set; }
        public string NameStatus { get; set; } = null!;
    }
}
