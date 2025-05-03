using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public DateTime DateBirth { get; set; }
        public DateTime? DateDeath { get; set; }
        public string FullName => $"{Lastname} {Firstname} {Middlename ?? ""}".Trim();
    }
}
