using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public int IdDocumentType { get; set; }
        public string DocumentTypeName { get; set; }
        public int? IdExhibition { get; set; }
        public string ExhibitionName { get; set; }
        public List<ExhibitMinimalModel> Exhibits { get; set; } = new List<ExhibitMinimalModel>();
    }
}
