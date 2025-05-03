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
        BaseViewModel CurrentPageViewModel { get; }
        event Action CurrentPageViewModelChanged;
        void NavigateTo<TViewModel>() where TViewModel : BaseViewModel;
        void NavigateToWelcomePage();
        string CurrentPageTitle { get; }
        event Action CurrentPageTitleChanged;
        bool IsCloseButtonVisible { get; }
        event Action IsCloseButtonVisibleChanged;
    }
}
