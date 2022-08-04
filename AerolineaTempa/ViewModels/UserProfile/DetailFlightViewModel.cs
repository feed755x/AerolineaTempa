using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AerolineaTempa.Helpers;
using AerolineaTempa.Interfaces;
using AerolineaTempa.Models;
using AerolineaTempa.RestService.Models;
using AerolineaTempa.ViewModels.Base;
using AerolineaTempa.ViewModels.Flight;
using AerolineaTempa.ViewModels.Home;
using Microsoft.Extensions.DependencyInjection;
using MvvmHelpers.Commands;
using Newtonsoft.Json;

namespace AerolineaTempa.ViewModels.UserProfile
{
    public class DetailFlightViewModel : BaseViewModel
    {
        public DetailFlightViewModel(INavigationService navigationService)
        {
            _navegationService = navigationService;
        }
        #region Services
        private INavigationService _navegationService;
        #endregion

        #region Properties
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

        private int _idViaje;

        public int idViaje
        {
            get => _idViaje;
            set
            {
                _idViaje = value;
                OnPropertyChanged();
            }
        }

        private string _idvuelo;

        public string idVuelo
        {
            get => _idvuelo;
            set
            {
                _idvuelo = value;
                OnPropertyChanged();
            }
        }

        private string _aerolinea;

        public string Aerolinea
        {
            get => _aerolinea;
            set
            {
                _aerolinea = value;
                OnPropertyChanged();
            }
        }

        private string _origen;

        public string Origen
        {
            get => _origen;
            set
            {
                _origen = value;
                OnPropertyChanged();
            }
        }

        private string _destino;

        public string Destino
        {
            get => _destino;
            set
            {
                _destino = value;
                OnPropertyChanged();
            }
        }

        private string _fechaSalida;

        public string FechaSalida
        {
            get => _fechaSalida;
            set
            {
                _fechaSalida = value;
                OnPropertyChanged();
            }
        }

        private string _horaSalida;

        public string HoraSalida
        {
            get => _horaSalida;
            set
            {
                _horaSalida = value;
                OnPropertyChanged();
            }
        }

        private string _fechaLlegada;

        public string FechaLlegada
        {
            get => _fechaLlegada;
            set
            {
                _fechaLlegada = value;
                OnPropertyChanged();
            }
        }

        private string _horaLlegada;

        public string HoraLlegada
        {
            get => _horaLlegada;
            set
            {
                _horaLlegada = value;
                OnPropertyChanged();
            }
        }

        private int _asientosComprados;

        public int AsientosComprados
        {
            get => _asientosComprados;
            set
            {
                _asientosComprados = value;
                OnPropertyChanged();
            }
        }

        private int _asientosDisponibles;

        public int AsientosDisponibles
        {
            get => _asientosDisponibles;
            set
            {
                _asientosDisponibles = value;
                OnPropertyChanged();
            }
        }

        private double _precioTotal;

        public double PrecioTotal
        {
            get => _precioTotal;
            set
            {
                _precioTotal = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand CancelItemCommand
        {
            get
            {
                return new AsyncCommand(CancelItem);
            }
        }
        #endregion

        #region Methods
        public async Task CancelItem()
        {
            if (true)
            {
                UserDialogs.Instance.ShowLoading("Cargando");

                try
                {
                    
                    DeleteTripRequest DeleteTrip = new DeleteTripRequest();
                    DeleteTrip.isCanceled = 1;
                    
                    string urlDelete = Constants.CONTS_URL_BASE_SERVICES + Constants.CONTS_CONTROLLER_DELETE_TRIP + idViaje.ToString();
                    string urlUpdate = Constants.CONTS_URL_BASE_SERVICES + Constants.CONTS_CONTROLLER_UPDATE_FLIGHT + idVuelo.ToString();

                    HttpClient client = new HttpClient();

                    //Eliminar viaje del usuario
                    string jsonData = JsonConvert.SerializeObject(DeleteTrip);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(urlDelete, content);
                    string result = await response.Content.ReadAsStringAsync();

                    await Container.Current.Services.GetRequiredService<UserProfileViewModel>().GetTrips();

                    //Actualizar boletos disponibles del vuelo
                    UpdatePlacesRequest UpdatePlace = new UpdatePlacesRequest();
                    UpdatePlace.asientosDisponibles = AsientosDisponibles + AsientosComprados;

                    string jsonData2 = JsonConvert.SerializeObject(UpdatePlace);
                    StringContent content2 = new StringContent(jsonData2, Encoding.UTF8, "application/json");
                    HttpResponseMessage response2 = await client.PostAsync(urlUpdate, content2);

                    string result2 = await response2.Content.ReadAsStringAsync();

                    await Container.Current.Services.GetRequiredService<AvailableFlightsViewModel>().GetFlights();
                    await Container.Current.Services.GetRequiredService<HomeTempaViewModel>().GetAllFlights();

                    await Task.Delay(1000);
                    UserDialogs.Instance.HideLoading();

                    await _navegationService.GoBackPop();
                }
                catch (Exception exception)
                {
                    UserDialogs.Instance.HideLoading();
                }

            }
            else
            {

            }
        }
        #endregion
    }
}
