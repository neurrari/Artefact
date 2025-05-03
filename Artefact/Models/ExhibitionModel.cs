using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class ExhibitionModel
    {
        public int Id { get; set; }
        public string NameExhibition { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public int IdHall { get; set; }
        public string HallNumber { get; set; }
    }
}
