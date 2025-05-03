using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public AccountReadModel User { get; set; } // Используем ReadModel из DTO, т.к. это ответ API
    }
}
