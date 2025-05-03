using Artefact.Commands;
using Artefact.Services.Navigation;
using Artefact.ViewModels.Base;
using System;
using System.Windows.Input;
using System.Windows;
using Artefact.ViewModels.Pages;
using Artefact.Services;

namespace Artefact.ViewModels.Windows
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthService _authService;
        private string _currentPageTitle;
        private bool _isCloseButtonVisible;

        public BaseViewModel CurrentPageViewModel => _navigationService.CurrentPageViewModel;
        public string CurrentPageTitle { get => _currentPageTitle; set => SetProperty(ref _currentPageTitle, value); }
        public bool IsCloseButtonVisible { get => _isCloseButtonVisible; set => SetProperty(ref _isCloseButtonVisible, value); }

        private bool IsAdmin => _authService.IsLoggedIn && _authService.CurrentUser?.RoleName?.Equals("admin", StringComparison.OrdinalIgnoreCase) == true;
        private bool IsUser => _authService.IsLoggedIn && _authService.CurrentUser?.RoleName?.Equals("user", StringComparison.OrdinalIgnoreCase) == true;

        public Visibility DashboardVisibility => Visibility.Visible;
        public Visibility FundsVisibility => Visibility.Visible;
        public Visibility TempStoreVisibility => Visibility.Visible;
        public Visibility ReportsVisibility => Visibility.Visible;
        public Visibility RoomsVisibility => Visibility.Visible;
        public Visibility HelpVisibility => Visibility.Visible;
        public Visibility UsersVisibility => IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        public Visibility DictionariesVisibility => IsAdmin ? Visibility.Visible : Visibility.Collapsed;

        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToFundsCommand { get; }
        public ICommand NavigateToTempStoreCommand { get; }
        public ICommand NavigateToReportsCommand { get; }
        public ICommand NavigateToRoomsCommand { get; }
        public ICommand NavigateToHelpCommand { get; }
        public ICommand NavigateToUsersCommand { get; }
        public ICommand NavigateToDictionariesCommand { get; }
        public ICommand ClosePageCommand { get; }
        public ICommand LogoutCommand { get; }


        public MainWindowViewModel(INavigationService navigationService, IAuthService authService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));

            // Подписываемся на события
            _navigationService.CurrentPageViewModelChanged += OnCurrentPageViewModelChanged;
            _navigationService.CurrentPageTitleChanged += OnCurrentPageTitleChanged;
            _navigationService.IsCloseButtonVisibleChanged += OnIsCloseButtonVisibleChanged;
            _authService.AuthenticationStateChanged += OnAuthenticationStateChanged;

            NavigateToDashboardCommand = new RelayCommand(o => _navigationService.NavigateTo<DashboardPageViewModel>(), o => CanNavigate());
            NavigateToFundsCommand = new RelayCommand(o => _navigationService.NavigateTo<FundsPageViewModel>(), o => CanNavigate());
            NavigateToTempStoreCommand = new RelayCommand(o => _navigationService.NavigateTo<TempStorePageViewModel>(), o => CanNavigate());
            NavigateToReportsCommand = new RelayCommand(o => _navigationService.NavigateTo<ReportsPageViewModel>(), o => CanNavigate());
            NavigateToRoomsCommand = new RelayCommand(o => _navigationService.NavigateTo<RoomsPageViewModel>(), o => CanNavigate());
            NavigateToHelpCommand = new RelayCommand(o => _navigationService.NavigateTo<HelpPageViewModel>(), o => CanNavigate());
            
            NavigateToUsersCommand = new RelayCommand(o => _navigationService.NavigateTo<UsersPageViewModel>(), o => CanNavigate() && IsAdmin);
            NavigateToDictionariesCommand = new RelayCommand(o => _navigationService.NavigateTo<DictionariesPageViewModel>(), o => CanNavigate() && IsAdmin);

            ClosePageCommand = new RelayCommand(o => _navigationService.NavigateToWelcomePage(), o => _navigationService.IsCloseButtonVisible);
            LogoutCommand = new RelayCommand(o => PerformLogout(), o => _authService.IsLoggedIn);

            UpdateNavigationProperties();
            UpdateRoleBasedProperties();
        }

        // Метод для выхода
        private void PerformLogout()
        {
            _authService.Logout();
            _navigationService.NavigateToWelcomePage();
        }

        private bool CanNavigate() => _authService.IsLoggedIn;

        private void OnAuthenticationStateChanged()
        {
            UpdateRoleBasedProperties();

            if (!_authService.IsLoggedIn)
            {
                _navigationService.NavigateToWelcomePage();
            }
            else
            {
                _navigationService.NavigateTo<DashboardPageViewModel>();
            }
        }

        // Обновляет свойства, зависящие от роли и статуса входа
        private void UpdateRoleBasedProperties()
        {
            (NavigateToDashboardCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (NavigateToFundsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (NavigateToTempStoreCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (NavigateToReportsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (NavigateToRoomsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (NavigateToHelpCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (NavigateToUsersCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (NavigateToDictionariesCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (LogoutCommand as RelayCommand)?.RaiseCanExecuteChanged();

            OnPropertyChanged(nameof(UsersVisibility));
            OnPropertyChanged(nameof(DictionariesVisibility));

            Console.WriteLine($"MainWindowVM: Auth state changed. IsLoggedIn: {_authService.IsLoggedIn}, IsAdmin: {IsAdmin}");
        }


        // --- Существующие обработчики событий NavigationService ---
        private void OnCurrentPageViewModelChanged() => OnPropertyChanged(nameof(CurrentPageViewModel));
        private void OnCurrentPageTitleChanged() => CurrentPageTitle = _navigationService.CurrentPageTitle;
        private void OnIsCloseButtonVisibleChanged()
        {
            IsCloseButtonVisible = _navigationService.IsCloseButtonVisible;
            (ClosePageCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
        private void UpdateNavigationProperties()
        {
            OnCurrentPageViewModelChanged();
            OnCurrentPageTitleChanged();
            OnIsCloseButtonVisibleChanged();
        }

        // Метод SetUserRole больше не нужен, т.к. роль берется из AuthService

        // Удаляем метод Dispose() полностью
    }
}