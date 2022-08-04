using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AerolineaTempa.Interfaces;
using AerolineaTempa.Models;
using AerolineaTempa.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using MvvmHelpers.Commands;

namespace AerolineaTempa.ViewModels.Flight
{
    public class AvailableFlightsViewModel : BaseViewModel
    {
        public AvailableFlightsViewModel(INavigationService navigationService)
        {
            _navegationService = navigationService;

            ListFlights.Clear();
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 3 });
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 42 });
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 12 });
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 22 });
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 2 });
        }

        #region Services
        private INavigationService _navegationService;
        #endregion

        #region Properties
        private ObservableCollection<FlightModel> _listFlights = new ObservableCollection<FlightModel>();

        public ObservableCollection<FlightModel> ListFlights
        {
            get => _listFlights;
            set
            {
                _listFlights = value;
                OnPropertyChanged();
            }
        }

        private FlightModel _selectedFlight;

        public FlightModel SelectedFlight
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
                var vmBuy = Container.Current.Services.GetRequiredService<BuyFlightViewModel>();

                vmBuy.Aerolinea = SelectedFlight.aerolinea;
                vmBuy.Origen = SelectedFlight.origen;
                vmBuy.FechaSalida = SelectedFlight.fechaSalida;
                vmBuy.FechaLlegada = SelectedFlight.fechaLlegada;
                vmBuy.Destino = SelectedFlight.destino;
                vmBuy.AsientosDisponibles = SelectedFlight.asientosDisponibles;
                vmBuy.PrecioAsiento = SelectedFlight.precioAsiento;

                vmBuy.NumeroAsientos = 0;
                vmBuy.TotalCobro = 0;

                await _navegationService.NavigateModalAsync("BuyFlight");

                SelectedFlight = null;
            }
        }
        #endregion
    }
}
