using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Task
{
    public class TaskUpdateDto
    {
        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }

        public DateTime? DateDeadline { get; set; }

        public int? StatusId { get; set; }

        public int? AssignedId { get; set; }
    }
}