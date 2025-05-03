using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Account
{
    public class AccountLoginModel
    {
        [Required]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
