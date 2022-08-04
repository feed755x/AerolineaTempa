using System;
using System.Threading.Tasks;

namespace AerolineaTempa.Interfaces
{
    public interface INavigationService
    {
        string CurrentPageKey { get; }

        void Configure(string pageKey, Type pageType);
        void init();
        void SetRootPage(string rootPageKey);
        Task GoBack();
        Task GoBackPop();
        Task NavigateModalAsync(string pageKey, bool animated = true);
        Task NavigateModalAsync(string pageKey, object parameter, bool animated = true);
        Task NavigateAsync(string pageKey, bool animated = true);
        Task NavigateAsync(string pageKey, object parameter, bool animated = true);
    }
}
