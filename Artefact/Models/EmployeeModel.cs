using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Phone { get; set; }
        public string FullName => $"{Lastname} {Firstname} {Middlename ?? ""}".Trim();
    }
}
