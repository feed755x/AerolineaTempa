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
using Xamarin.Forms;

namespace AerolineaTempa.ViewModels.Home
{
    public class HomeTempaViewModel : BaseViewModel
    {

        public HomeTempaViewModel(INavigationService navigationService)
        {
            _navegationService = navigationService;
            GetAllFlights();
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
        public ICommand AddItemCommand
        {
            get
            {
                return new AsyncCommand(AddItem);
            }
        }

        public ICommand EditItemCommand
        {
            get
            {
                return new AsyncCommand(EditItem);
            }
        }
        #endregion

        #region Methods
        public async Task AddItem()
        {
            var vmBuy = Container.Current.Services.GetRequiredService<AddFlightViewModel>();

            vmBuy.Aerolinea = "";
            vmBuy.Origen = "";
            vmBuy.FechaSalida = DateTime.Now;
            vmBuy.FechaLlegada = DateTime.Now;
            vmBuy.Destino = "";
            vmBuy.AsientosDisponibles = 0;
            vmBuy.PrecioAsiento = 0;

            await _navegationService.NavigateModalAsync("AddFlight");
        }

        public async Task EditItem()
        {
            if (SelectedFlight != null)
            {
                var vmBuy = Container.Current.Services.GetRequiredService<EditFlightViewModel>();
                vmBuy.idVuelo = SelectedFlight.idVuelo;
                vmBuy.Aerolinea = SelectedFlight.aerolinea;
                vmBuy.Origen = SelectedFlight.origen;
                vmBuy.FechaSalida = DateTime.Now;
                vmBuy.FechaLlegada = DateTime.Now;
                vmBuy.Destino = SelectedFlight.destino;
                vmBuy.AsientosDisponibles = SelectedFlight.asientosDisponibles;
                vmBuy.PrecioAsiento = SelectedFlight.precioAsiento;

                await _navegationService.NavigateModalAsync("EditFlight");
                SelectedFlight = null;
            }
        }

        public async Task GetAllFlights()
        {
            ListFlights.Clear();

            try
            {
                string url = Constants.CONTS_URL_BASE_SERVICES+Constants.CONTS_CONTROLLER_ALL_FLIGHTS;
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
