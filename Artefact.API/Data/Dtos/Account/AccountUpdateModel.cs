using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Account
{
    public class AccountUpdateModel
    {
        [MaxLength(10)]
        public string? Login { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        public int? IdEmployee { get; set; }
        public int? IdRole { get; set; }
    }
}
