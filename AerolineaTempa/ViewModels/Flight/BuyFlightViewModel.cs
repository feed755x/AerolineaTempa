using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AerolineaTempa.Helpers;
using AerolineaTempa.Interfaces;
using AerolineaTempa.RestService.Models;
using AerolineaTempa.ViewModels.Base;
using AerolineaTempa.ViewModels.Home;
using AerolineaTempa.ViewModels.UserProfile;
using Microsoft.Extensions.DependencyInjection;
using MvvmHelpers.Commands;
using Newtonsoft.Json;

namespace AerolineaTempa.ViewModels.Flight
{
    public class BuyFlightViewModel : BaseViewModel
    {
        public BuyFlightViewModel(INavigationService navigationService)
        {
            _navegationService = navigationService;
        }

        #region Services
        private INavigationService _navegationService;
        #endregion

        #region Properties
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

        private string _fechaLlegada;

        public string FechaLlegada
        {
            get => _fechaLlegada;
            set
            {
                _fechaLlegada= value;
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

        private double _precioAsiento;

        public double PrecioAsiento
        {
            get => _precioAsiento;
            set
            {
                _precioAsiento = value;
                OnPropertyChanged();
            }
        }

        private int _numeroAsientos;

        public int NumeroAsientos
        {
            get => _numeroAsientos;
            set
            {
                _numeroAsientos = value;
                OnPropertyChanged();
            }
        }

        private double _totalCobro;

        public double TotalCobro
        {
            get => _totalCobro;
            set
            {
                _totalCobro = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand BuyItemCommand
        {
            get
            {
                return new AsyncCommand(BuyItem);
            }
        }

        public ICommand AddTicketCommand
        {
            get
            {
                return new AsyncCommand(AddTicket);
            }
        }
        #endregion

        #region Methods
        public async Task BuyItem()
        {
            if (NumeroAsientos > 0)
            {
                UserDialogs.Instance.ShowLoading("Cargando");

                try
                {
                    string url = Constants.CONTS_URL_BASE_SERVICES + Constants.CONTS_CONTROLLER_UPDATE_FLIGHT + idVuelo.ToString();
                    string urlAdd = Constants.CONTS_URL_BASE_SERVICES + Constants.CONTS_CONTROLLER_ADD_TRIP;
                    HttpClient client = new HttpClient();
                    //Se registra nuevo viaje
                    AddTripRequest addTrip = new AddTripRequest();
                    addTrip.idPasajero = 1;
                    addTrip.idVuelo = Int32.Parse(idVuelo);
                    addTrip.asientosComprados = NumeroAsientos;
                    addTrip.precioTotal = ((int)TotalCobro);
                    addTrip.isCanceled = 0;

                    string jsonData2 = JsonConvert.SerializeObject(addTrip);
                    StringContent content2 = new StringContent(jsonData2, Encoding.UTF8, "application/json");
                    HttpResponseMessage response2 = await client.PostAsync(urlAdd, content2);

                    string result2 = await response2.Content.ReadAsStringAsync();

                    //Se actualiza los vuelos con los asientos comprados
                    UpdatePlacesRequest UpdatePlace = new UpdatePlacesRequest();
                    UpdatePlace.asientosDisponibles = AsientosDisponibles;

                    string jsonData = JsonConvert.SerializeObject(UpdatePlace);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    string result = await response.Content.ReadAsStringAsync();

                    await Container.Current.Services.GetRequiredService<AvailableFlightsViewModel>().GetFlights();
                    await Container.Current.Services.GetRequiredService<HomeTempaViewModel>().GetAllFlights();
                    await Container.Current.Services.GetRequiredService<UserProfileViewModel>().GetTrips();

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
                return;
            }            
        }

        public async Task AddTicket()
        {
            if (AsientosDisponibles > 0)
            {
                AsientosDisponibles--;
                NumeroAsientos++;

                TotalCobro = PrecioAsiento * NumeroAsientos;
            }
            else
            {
                return;
            }
            
        }
        #endregion
    }
}
