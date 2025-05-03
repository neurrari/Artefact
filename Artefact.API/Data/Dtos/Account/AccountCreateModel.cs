using System.ComponentModel.DataAnnotations;

namespace Artefact.API.Data.Dtos.Account
{
    public class AccountCreateModel
    {
        [Required]
        [MaxLength(10)]
        public string Login { get; set; } = null!;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        // DateCreated handled by DB

        [Required]
        public int IdEmployee { get; set; }
        [Required]
        public int IdRole { get; set; }
    }
}
