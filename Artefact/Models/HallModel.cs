using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class HallModel
    {
        public int Id { get; set; }
        public string NumberHall { get; set; }
        public int IdCaretaker { get; set; }
        public string CaretakerFullName { get; set; } // Имя смотрителя (получаем из API)
    }
}
