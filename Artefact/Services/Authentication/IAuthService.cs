using Artefact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Services
{
    public interface IAuthService
    {
        AccountModel CurrentUser { get; }

        bool IsLoggedIn { get; }

        Task<bool> LoginAsync(string login, string password);

        void Logout();

        event Action AuthenticationStateChanged;
    }
}
