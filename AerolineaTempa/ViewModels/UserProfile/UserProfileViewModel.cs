using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AerolineaTempa.Interfaces;
using AerolineaTempa.Models;
using AerolineaTempa.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using MvvmHelpers.Commands;

namespace AerolineaTempa.ViewModels.UserProfile
{
    public class UserProfileViewModel : BaseViewModel
    {
        public UserProfileViewModel(INavigationService navigationService)
        {
            _navegationService = navigationService;
            ListFlights.Clear();
            ListFlights.Add(new UserFlightsModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosComprados = 1, precioTotal = 212 });
            ListFlights.Add(new UserFlightsModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosComprados = 2, precioTotal = 414 });
            ListFlights.Add(new UserFlightsModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosComprados = 1, precioTotal = 212 });
            ListFlights.Add(new UserFlightsModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosComprados = 1, precioTotal = 212 });
            ListFlights.Add(new UserFlightsModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosComprados = 1, precioTotal = 212 });
        }

        #region Services
        private INavigationService _navegationService;
        #endregion

        #region Properties
        private ObservableCollection<UserFlightsModel> _listFlights = new ObservableCollection<UserFlightsModel>();

        public ObservableCollection<UserFlightsModel> ListFlights
        {
            get => _listFlights;
            set
            {
                _listFlights = value;
                OnPropertyChanged();
            }
        }

        private UserFlightsModel _selectedFlight;

        public UserFlightsModel SelectedFlight
        {
            get => _selectedFlight;
            set
            {
                _selectedFlight = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand SelectItemCommand
        {
            get
            {
                return new AsyncCommand(SelectItem);
            }
        }
        #endregion

        #region Methods
        public async Task SelectItem()
        {
            if (SelectedFlight != null)
            {
                var vmBuy = Container.Current.Services.GetRequiredService<DetailFlightViewModel>();
                vmBuy.SelectedFlight = SelectedFlight;
                vmBuy.Aerolinea = SelectedFlight.aerolinea;
                vmBuy.Origen = SelectedFlight.origen;
                vmBuy.FechaSalida = SelectedFlight.fechaSalida;
                vmBuy.Destino = SelectedFlight.destino;
                vmBuy.FechaLlegada = SelectedFlight.fechaLlegada;
                vmBuy.AsientosComprados = SelectedFlight.asientosComprados;
                vmBuy.PrecioTotal = SelectedFlight.precioTotal;

                await _navegationService.NavigateModalAsync("DetailFlight");
                SelectedFlight = null;
            }
        }
        #endregion
    }
}
