using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Task
{
    public class TaskCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = null!;

        // DateCreated handled by DB

        [Required]
        public DateTime DateDeadline { get; set; }
        [Required]
        public int IdStatus { get; set; }
        [Required]
        public int IdCreator { get; set; }
        public int? IdAssigned { get; set; }
    }
}
