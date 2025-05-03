using Artefact.Commands;
using Artefact.Models;
using Artefact.Services.Api;
using Artefact.Services;
using Artefact.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Artefact.ViewModels.Pages
{
    public class FundsPageViewModel : PageViewModelBase
    {
        public override string Title => "Основной фонд";
        private readonly IApiClient _apiClient;
        private readonly IAuthService _authService;
        private readonly bool _isAdmin;

        public ObservableCollection<CollectionModel> Collections { get; } = new ObservableCollection<CollectionModel>(); // Коллекция для отображения

        public FundsPageViewModel(IApiClient apiClient, IAuthService authService)
        {
            _apiClient = apiClient;
            _authService = authService;
            _isAdmin = _authService.CurrentUserRole == "admin";

            LoadCollections();
        }

        private async void LoadCollections()
        {
            Collections.Clear();
            IEnumerable<CollectionModel> loadedCollections;

            if (_isAdmin)
            {
                loadedCollections = await _apiClient.GetAllCollectionsAsync();
            }
            else
            {
                var userId = _authService.CurrentUserId;
                loadedCollections = await _apiClient.GetUserCollectionsAsync(userId); 
                var permittedCollections = await _apiClient.GetPermittedCollectionsAsync(userId);
                loadedCollections = loadedCollections.Union(permittedCollections).Distinct();
            }

            if (loadedCollections != null)
            {
                foreach (var collection in loadedCollections)
                {
                    Collections.Add(collection);
                }
            }
        }

        // Добавьте команды для работы с коллекциями (добавление, редактирование, удаление - если нужно)
        // Например, команда для выдачи разрешения (только для админа)
        public ICommand GrantPermissionCommand => new RelayCommand(GrantPermission, CanGrantPermission);

        private async void GrantPermission(object parameter)
        {
            if (parameter is CollectionModel collection && _isAdmin)
            {
                // Логика выбора пользователя, которому дается разрешение
                // var selectedUser = ...;
                // await _apiClient.GrantCollectionPermissionAsync(collection.Id, selectedUser.Id);
                // Обновить данные или выдать сообщение
            }
        }

        private bool CanGrantPermission(object parameter) => _isAdmin && parameter is CollectionModel;
    }
}
