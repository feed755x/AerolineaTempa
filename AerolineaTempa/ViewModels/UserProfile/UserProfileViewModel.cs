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

namespace AerolineaTempa.ViewModels.UserProfile
{
    public class UserProfileViewModel : BaseViewModel
    {
        public UserProfileViewModel(INavigationService navigationService)
        {
            _navegationService = navigationService;
            GetTrips();
        }

        #region Services
        private INavigationService _navegationService;
        #endregion

        #region Properties
        private ObservableCollection<TripModel> _listFlights = new ObservableCollection<TripModel>();

        public ObservableCollection<TripModel> ListFlights
        {
            get => _listFlights;
            set
            {
                _listFlights = value;
                OnPropertyChanged();
            }
        }

        private TripModel _selectedFlight;

        public TripModel SelectedFlight
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
                vmBuy.idViaje = SelectedFlight.idViaje;
                vmBuy.idVuelo = SelectedFlight.idVuelo.ToString();
                vmBuy.SelectedFlight = SelectedFlight;
                vmBuy.Aerolinea = SelectedFlight.aerolinea;
                vmBuy.Origen = SelectedFlight.origen;
                vmBuy.FechaSalida = SelectedFlight.fechaSalida;
                vmBuy.Destino = SelectedFlight.destino;
                vmBuy.FechaLlegada = SelectedFlight.fechaLlegada;
                vmBuy.AsientosDisponibles = SelectedFlight.asientosDisponibles;
                vmBuy.AsientosComprados = SelectedFlight.asientosComprados;
                vmBuy.PrecioTotal = SelectedFlight.precioTotal;

                await _navegationService.NavigateModalAsync("DetailFlight");
                SelectedFlight = null;
            }
        }

        public async Task GetTrips()
        {
            ListFlights.Clear();

            try
            {
                string url = Constants.CONTS_URL_BASE_SERVICES + Constants.CONTS_CONTROLLER_TRIPS_GET;
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);

                var response = JsonConvert.DeserializeObject<List<TripModel>>(result);

                ListFlights = new ObservableCollection<TripModel>(response);
            }
            catch (Exception exception)
            {

            }

        }
        #endregion
    }
}
