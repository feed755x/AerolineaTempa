using AerolineaTempa.Interfaces;
using AerolineaTempa.Servicios;
using AerolineaTempa.ViewModels.Flight;
using AerolineaTempa.ViewModels.Home;
using AerolineaTempa.ViewModels.UserProfile;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace AerolineaTempa
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            
            var services = new ServiceCollection();
            
            services.AddSingleton<INavigationService, NavegationService>();
            services.AddSingleton<IMessageService, MessageService>();

            services.AddSingleton<HomeTempaViewModel>();
            services.AddSingleton<AddFlightViewModel>();
            services.AddSingleton<EditFlightViewModel>();

            services.AddSingleton<AvailableFlightsViewModel>();
            services.AddSingleton<BuyFlightViewModel>();

            services.AddSingleton<UserProfileViewModel>();
            services.AddSingleton<DetailFlightViewModel>();

            var serviceProvider = services.BuildServiceProvider(validateScopes: true);

            var scope = serviceProvider.CreateScope();

            Container.Current.Services = serviceProvider;

            var NavegationService = serviceProvider.GetService<INavigationService>();
            NavegationService.init();
            NavegationService.SetRootPage(nameof(RootHomeTabbed));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
