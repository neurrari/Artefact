using Artefact.API.Data.Dtos.Account;

namespace Artefact.API.Services.Interfaces
{
    public interface IAccountService : ICrudService<AccountReadModel, AccountCreateModel, AccountUpdateModel> { }
}
