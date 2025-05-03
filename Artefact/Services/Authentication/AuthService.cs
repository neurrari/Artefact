using Artefact.Models;
using Artefact.Services.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IApiClient _apiClient;
        private AccountModel _currentUser;

        public AccountModel CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                AuthenticationStateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentUser != null;

        public event Action AuthenticationStateChanged;

        public AuthService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> LoginAsync(string login, string password)
        {
            try
            {
                var loginResponse = await _apiClient.LoginAsync(login, password);

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    _apiClient.SetAuthToken(loginResponse.Token);

                    CurrentUser = MapToAccountModel(loginResponse.User);

                    Console.WriteLine($"AuthService: User {CurrentUser.Login} logged in successfully. Role: {CurrentUser.RoleName}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"AuthService: Login failed for user {login}. Invalid credentials or API error.");
                    Logout();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AuthService: Exception during login for user {login}. Error: {ex.Message}");
                Logout();

                return false;
            }
        }

        public void Logout()
        {
            CurrentUser = null;
            _apiClient.ClearAuthToken();
            Console.WriteLine("AuthService: User logged out.");
        }

        // Метод для маппинга DTO из API в локальную модель WPF
        private AccountModel MapToAccountModel(AccountReadModel dto)
        {
            if (dto == null) return null;
            return new AccountModel
            {
                Id = dto.Id,
                Login = dto.Login,
                DateCreated = dto.DateCreated,
                IdEmployee = dto.IdEmployee,
                EmployeeFullName = dto.EmployeeFullName,
                IdRole = dto.IdRole,
                RoleName = dto.RoleName
            };
        }
    }

}
