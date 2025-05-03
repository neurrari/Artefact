using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDeadline { get; set; }
        public int IdStatus { get; set; }
        public string StatusName { get; set; } // Название статуса (из API)
        public int IdCreator { get; set; }
        public string CreatorFullName { get; set; } // Имя создателя (из API)
        public int? IdAssigned { get; set; }
        public string AssignedFullName { get; set; } // Имя назначенного (из API)
    }
}
