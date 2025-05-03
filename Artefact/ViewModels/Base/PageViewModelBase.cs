using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artefact.ViewModels.Base
{
    public abstract class PageViewModelBase : BaseViewModel
    {
        public abstract string Title { get; }
    }
}
