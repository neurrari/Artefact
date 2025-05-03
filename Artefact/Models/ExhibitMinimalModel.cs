using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class ExhibitMinimalModel
    {
        public int Id { get; set; }
        public string NameExhibit { get; set; }
        public int? InventoryNumber { get; set; }
        public string InventoryCypherName { get; set; }
    }
}
