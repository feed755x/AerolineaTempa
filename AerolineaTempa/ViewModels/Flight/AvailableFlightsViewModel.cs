using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using AerolineaTempa.Helpers;
using AerolineaTempa.Interfaces;
using AerolineaTempa.Models;
using AerolineaTempa.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using MvvmHelpers.Commands;
using Newtonsoft.Json;

namespace AerolineaTempa.ViewModels.Flight
{
    public class AvailableFlightsViewModel : BaseViewModel
    {
        public AvailableFlightsViewModel(INavigationService navigationService)
        {
            _navegationService = navigationService;
            GetFlights();
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

                vmBuy.idVuelo = SelectedFlight.idVuelo;
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

        public async Task GetFlights()
        {
            ListFlights.Clear();

            try
            {
                string url = Constants.CONTS_URL_BASE_SERVICES + Constants.CONTS_CONTROLLER_FLIGHTS;
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);

                var response = JsonConvert.DeserializeObject<List<FlightModel>>(result);

                ListFlights = new ObservableCollection<FlightModel>(response);
            }
            catch (Exception exception)
            {

            }

        }
        #endregion
    }
}
