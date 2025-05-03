namespace Artefact.API.Data.Dtos.Account
{
    public class LoginResponseModel
    {
        public string Token { get; set; } = null!;
        public AccountReadModel User { get; set; } = null!;
    }
}
