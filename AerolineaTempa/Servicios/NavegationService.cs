
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AerolineaTempa.Interfaces;
using AerolineaTempa.Views.Flight;
using AerolineaTempa.Views.Home;
using AerolineaTempa.Views.UserProfile;
using Xamarin.Forms;

namespace AerolineaTempa.Servicios
{
    public class NavegationService : INavigationService
    {
        #region Properties

        private readonly object _sync = new object();

        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();

        private readonly Stack<NavigationPage> _navigationPageStack = new Stack<NavigationPage>();

        private NavigationPage CurrentNavigationPage => _navigationPageStack.Peek();

        #endregion

        #region Methods

        /// <summary>
        /// Metodo para configurar el servicio de navegacion
        /// </summary>
        /// <param name="pageKey">Clave de la page</param>
        /// <param name="pageType">Type de la page</param>
        public void Configure(string pageKey, Type pageType)
        {
            lock (_sync)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        /// <summary>
        /// Metodo para incializar el servicio.
        /// </summary>
        public void init()
        {
            this.Configure(nameof(RootHomeTabbed), typeof(RootHomeTabbed));

            this.Configure(nameof(HomeTempa), typeof(HomeTempa));
            this.Configure(nameof(AddFlight),typeof(AddFlight));
            this.Configure(nameof(EditFlight), typeof(EditFlight));

            this.Configure(nameof(UserProfile), typeof(UserProfile));
            this.Configure(nameof(DetailFlight), typeof(DetailFlight));

            this.Configure(nameof(AvailableFlights), typeof(AvailableFlights));
            this.Configure(nameof(BuyFlight),typeof(BuyFlight));
            //this.SetRootPage(nameof(RootBroxchainTabbedPage));
            //App.Current.MainPage = mainPage;
        }

        /// <summary>
        /// Metodo para setear la pagina inicial o root
        /// </summary>
        /// <param name="rootPageKey">Clave de la pagina para el root</param>
        /// <returns></returns>
        public void SetRootPage(string rootPageKey)
        {
            var rootPage = GetPage(rootPageKey);
            _navigationPageStack.Clear();
            var mainPage = rootPage;
            App.Current.MainPage = mainPage;

        }

        /// <summary>
        /// Pagina actual de la navegacion
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                lock (_sync)
                {
                    if (CurrentNavigationPage?.CurrentPage == null)
                    {
                        return null;
                    }
                    var pageType = CurrentNavigationPage.CurrentPage.GetType();
                    return _pagesByKey.ContainsValue(pageType)
                    ? _pagesByKey.First(p => p.Value == pageType).Key
                    : null;
                }
            }
        }

        /// <summary>
        /// Regresar una pagina atras
        /// </summary>
        /// <returns>tarea de regreso</returns>
        public async Task GoBack()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            //TODO
            if (navigationStack.NavigationStack.Count > 1)
            {
                var pageLast = navigationStack.ModalStack.Last();
                
                    await CurrentNavigationPage.PopAsync();
                    return;

            }
            if (_navigationPageStack.Count > 1)
            {
                _navigationPageStack.Pop();
                await CurrentNavigationPage.Navigation.PopModalAsync();
                return;
            }
            await CurrentNavigationPage.PopAsync();
        }


        /// <summary>
        /// Regresar una pagina atras
        /// </summary>
        /// <returns>tarea de regreso</returns>
        public async Task GoBackPop()
        {
            var tabPageCurrent = (TabbedPage)App.Current.MainPage;

            if (tabPageCurrent.Navigation.ModalStack.Count > 0)
            {
                await tabPageCurrent.Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Metodo para navegar a una pagina en modo modal
        /// </summary>
        /// <param name="pageKey">Clave de la pagina</param>
        /// <param name="animated">Activar animacion de navegacion</param>
        /// <returns></returns>
        public async Task NavigateModalAsync(string pageKey, bool animated = true)
        {
            await NavigateModalAsync(pageKey, null, animated);
        }

        /// <summary>
        /// Metodo para navegar a una pagina en modo modal, enviandole parametros
        /// </summary>
        /// <param name="pageKey">Clave de la pagina</param>
        /// <param name="parameter">Parametro</param>
        /// <param name="animated">Activar animacion de navegacion</param>
        /// <returns></returns>
        public async Task NavigateModalAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            NavigationPage.SetHasNavigationBar(page, false);
            var modalNavigationPage = new NavigationPage(page);
            _navigationPageStack.Push(modalNavigationPage);
            var tabPageCurrent = (Page)App.Current.MainPage;
            await tabPageCurrent.Navigation.PushModalAsync(modalNavigationPage, animated);
        }

        /// <summary>
        /// Metodo para navegar a una pagina en el tab seleccionado
        /// </summary>
        /// <param name="pageKey">Clave de la pagina</param>
        /// <param name="animated">Activar animacion de navegacion</param>
        /// <returns></returns>
        public async Task NavigateAsync(string pageKey, bool animated = true)
        {
            await NavigateAsync(pageKey, null, animated);
        }

        /// <summary>
        /// Metodo para navegar a una pagina en el tab seleccionado enviando parametros
        /// </summary>
        /// <param name="pageKey">Clave de la pagina</param>
        /// <param name="parameter">Parametro</param>
        /// <param name="animated">Activar animacion de navegacion</param>
        /// <returns></returns>
        public async Task NavigateAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            var tabPageCurrent = (Page)App.Current.MainPage;
            NavigationPage.SetHasNavigationBar(page, true);

            if (tabPageCurrent is TabbedPage)
            {
                    await ((TabbedPage)tabPageCurrent).CurrentPage.Navigation.PushAsync(page, animated);
             
            }
            else
            {
                    await tabPageCurrent.Navigation.PushAsync(page, animated);
            }

        }


        /// <summary>
        /// Metodo para crear u obtener una pagina
        /// </summary>
        /// <param name="pageKey">Clave de la pagina</param>
        /// <param name="parameter">Parametro</param>
        /// <returns></returns>
        private Page GetPage(string pageKey, object parameter = null)
        {
            lock (_sync)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                    $"No se encuentra la pagina: {pageKey}. ¿Olvidaste llamar a NavigationService.Configure?");
                }
                var type = _pagesByKey[pageKey];
                ConstructorInfo constructor;
                object[] parameters;
                if (parameter == null)
                {
                    constructor = type.GetTypeInfo()
                                            .DeclaredConstructors
                                            .FirstOrDefault(c => !c.GetParameters().Any());
                    parameters = new object[]
                                        {
                                        };
                }
                else
                {
                    constructor = type.GetTypeInfo()
                                            .DeclaredConstructors
                                            .FirstOrDefault(
                    c =>
                    {
                        var p = c.GetParameters();
                        return p.Length == 1
    && p[0].ParameterType == parameter.GetType();
                    });
                    parameters = new[]
                                        {
parameter
                    };
                }
                if (constructor == null)
                {
                    throw new InvalidOperationException(
                    "No se ha encontrado ningún constructor adecuado para la página. " + pageKey);
                }
                var page = constructor.Invoke(parameters) as Page;
                return page;
            }
        }

        #endregion
    }
}
