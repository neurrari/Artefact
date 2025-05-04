using Artefact.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.Services.Navigation
{
    public interface INavigationService
    {
        event Action CurrentViewModelChanged;
        object CurrentViewModel { get; }
        void NavigateTo<T>() where T : PageViewModelBase;
        void NavigateToWelcome();
    }
}
