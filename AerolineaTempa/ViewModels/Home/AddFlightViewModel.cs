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
using AerolineaTempa.ViewModels.Flight;
using Microsoft.Extensions.DependencyInjection;
using MvvmHelpers.Commands;
using Newtonsoft.Json;

namespace AerolineaTempa.ViewModels.Home
{
    public class AddFlightViewModel : BaseViewModel
    {
        public AddFlightViewModel(INavigationService navigationService)
        {
            _navegationService = navigationService;
        }

        #region Services
        private INavigationService _navegationService;
        #endregion

        #region Properties
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

        private DateTime _fechaSalida;

        public DateTime FechaSalida
        {
            get => _fechaSalida;
            set
            {
                _fechaSalida = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _horaSalida;

        public TimeSpan HoraSalida
        {
            get => _horaSalida;
            set
            {
                _horaSalida = value;
                OnPropertyChanged();
            }
        }

        private DateTime _fechaLlegada;

        public DateTime FechaLlegada
        {
            get => _fechaLlegada;
            set
            {
                _fechaLlegada = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _horaLlegada;

        public TimeSpan HoraLlegada
        {
            get => _horaLlegada;
            set
            {
                _horaLlegada= value;
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
        #endregion

        #region Commands
        public ICommand SaveItemCommand
        {
            get
            {
                return new AsyncCommand(SaveItem);
            }
        }
        #endregion

        #region Methods
        public async Task SaveItem()
        {
            var fechaSalida = string.Format("{0:dd MMM}",FechaSalida);
            var horaSalida = HoraSalida.ToString(@"hh\:mm");
            var fechaLlegada = string.Format("{0:dd MMM}", FechaLlegada);
            var horaLlegada = HoraLlegada.ToString(@"hh\:mm");

            if (!string.IsNullOrEmpty(Aerolinea)
                && !string.IsNullOrEmpty(Origen)
                && !string.IsNullOrEmpty(fechaSalida)
                && !string.IsNullOrEmpty(horaSalida)
                && !string.IsNullOrEmpty(horaLlegada)
                && !string.IsNullOrEmpty(fechaLlegada)
                && !string.IsNullOrEmpty(Destino)
                && AsientosDisponibles > 0
                && PrecioAsiento > 0)
            {

                UserDialogs.Instance.ShowLoading("Cargando");

                try
                {
                    
                    string urlAdd = Constants.CONTS_URL_BASE_SERVICES + Constants.CONTS_CONTROLLER_ADD_FLIGHT;
                    HttpClient client = new HttpClient();
                    //Se registra nuevo viaje
                    AddFlightRequest addFlight = new AddFlightRequest();
                    addFlight.icon = "ic_flight_world.png";
                    addFlight.aerolinea = Aerolinea;
                    addFlight.origen = Origen;
                    addFlight.destino = Destino;
                    addFlight.fechaSalida = fechaSalida;
                    addFlight.fechaLlegada = fechaLlegada;
                    addFlight.horaLlegada = horaLlegada;
                    addFlight.horaSalida = horaSalida;
                    addFlight.asientosDisponibles = AsientosDisponibles;
                    addFlight.precioAsiento = PrecioAsiento;
                    addFlight.isAvailable = 1;

                    string jsonData2 = JsonConvert.SerializeObject(addFlight);
                    StringContent content2 = new StringContent(jsonData2, Encoding.UTF8, "application/json");
                    HttpResponseMessage response2 = await client.PostAsync(urlAdd, content2);

                    string result2 = await response2.Content.ReadAsStringAsync();

                    await Container.Current.Services.GetRequiredService<AvailableFlightsViewModel>().GetFlights();
                    await Container.Current.Services.GetRequiredService<HomeTempaViewModel>().GetAllFlights();

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
