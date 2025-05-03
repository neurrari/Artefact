using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class CollectionModel
    {
        public int Id { get; set; }
        public int IdInventoryCypher { get; set; }
        public string InventoryCypherName { get; set; }
        public string NameCollection { get; set; }
        public int IdEmployee { get; set; }
        public string EmployeeFullName { get; set; }
        public List<StorageModel> Storages { get; set; } = new List<StorageModel>();
    }
}
