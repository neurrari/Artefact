using Artefact.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Services.Navigation
{
    public class NavigationService : BaseViewModel, INavigationService
    {
        private BaseViewModel _currentPageViewModel;
        private readonly Func<Type, BaseViewModel> _viewModelFactory;
        private string _currentPageTitle;
        private bool _isCloseButtonVisible;

        public BaseViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            private set
            {
                _currentPageViewModel = value;
                OnPropertyChanged();
                CurrentPageViewModelChanged?.Invoke();
                UpdatePageProperties();
            }
        }

        public string CurrentPageTitle
        {
            get => _currentPageTitle;
            private set
            {
                _currentPageTitle = value;
                OnPropertyChanged();
                CurrentPageTitleChanged?.Invoke();
            }
        }

        public bool IsCloseButtonVisible
        {
            get => _isCloseButtonVisible;
            private set
            {
                _isCloseButtonVisible = value;
                OnPropertyChanged();
                IsCloseButtonVisibleChanged?.Invoke();
            }
        }

        public event Action CurrentPageViewModelChanged;
        public event Action CurrentPageTitleChanged;
        public event Action IsCloseButtonVisibleChanged;

        public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            BaseViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentPageViewModel = viewModel;
        }

        public void NavigateToWelcomePage()
        {
            NavigateTo<WelcomePageViewModel>();
        }

        private void UpdatePageProperties()
        {
            if (CurrentPageViewModel is WelcomePageViewModel)
            {
                CurrentPageTitle = string.Empty;
                IsCloseButtonVisible = false;
            }
            else if (CurrentPageViewModel is PageViewModelBase pageVm)
            {
                CurrentPageTitle = pageVm.Title;
                IsCloseButtonVisible = true;
            }
            else
            {
                CurrentPageTitle = "Default Title";
                IsCloseButtonVisible = true;
            }
        }
    }
}
