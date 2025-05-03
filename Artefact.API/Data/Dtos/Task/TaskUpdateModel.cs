using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Task
{
    public class TaskUpdateModel
    {
        [MaxLength(50)]
        public string? Title { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }
        public DateTime? DateDeadline { get; set; }
        public int? IdStatus { get; set; }

        // Creator usually cannot be changed

        public int? IdAssigned { get; set; }
    }
}
