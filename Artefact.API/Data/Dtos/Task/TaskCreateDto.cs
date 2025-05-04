using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Task
{
    public class TaskCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DateDeadline { get; set; }

        public int? StatusId { get; set; }

        public int? CreatorId { get; set; } 

        public int? AssignedId { get; set; } 
    }
}
