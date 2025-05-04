using Artefact.ViewModels.Base;
using Artefact.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public event Action CurrentViewModelChanged;

        public void NavigateTo<T>() where T : PageViewModelBase
        {
            CurrentViewModel = Activator.CreateInstance<T>();
        }

        public void NavigateToWelcome()
        {
            CurrentViewModel = new WelcomePageViewModel();
        }
    }
}
