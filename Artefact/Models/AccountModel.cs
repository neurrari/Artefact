using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateTime DateCreated { get; set; }
        public int IdEmployee { get; set; }
        public string EmployeeFullName { get; set; }
        public int IdRole { get; set; }
        public string RoleName { get; set; }
    }
}
