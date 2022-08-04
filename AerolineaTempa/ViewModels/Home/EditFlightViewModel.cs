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
    public class EditFlightViewModel : BaseViewModel
    {
        public EditFlightViewModel(INavigationService navigationService)
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
                _horaLlegada = value;
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

        public ICommand DeleteItemCommand
        {
            get
            {
                return new AsyncCommand(DeleteItem);
            }
        }
        #endregion

        #region Methods
        public async Task SaveItem()
        {
            var fechaSalida = string.Format("{0:dd MMM}", FechaSalida);
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

                    string urlAdd = Constants.CONTS_URL_BASE_SERVICES + Constants.CONTS_CONTROLLER_UPDATE_FLIGHT+idVuelo.ToString();
                    HttpClient client = new HttpClient();
                    //Se registra nuevo viaje
                    UpdateFlightRequest UpdateFlight = new UpdateFlightRequest();
                    UpdateFlight.aerolinea = Aerolinea;
                    UpdateFlight.origen = Origen;
                    UpdateFlight.destino = Destino;
                    UpdateFlight.fechaSalida = fechaSalida;
                    UpdateFlight.fechaLlegada = fechaLlegada;
                    UpdateFlight.horaLlegada = horaLlegada;
                    UpdateFlight.horaSalida = horaSalida;
                    UpdateFlight.asientosDisponibles = AsientosDisponibles;
                    UpdateFlight.precioAsiento = PrecioAsiento;

                    string jsonData2 = JsonConvert.SerializeObject(UpdateFlight);
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

        public async Task DeleteItem()
        {
            UserDialogs.Instance.ShowLoading("Cargando");

            try
            {

                DeleteFlightRequest DeleteFlight = new DeleteFlightRequest();
                DeleteFlight.isAvailable = 0;

                string urlDelete = Constants.CONTS_URL_BASE_SERVICES + Constants.CONTS_CONTROLLER_DELETE_FLIGHT + idVuelo.ToString();

                HttpClient client = new HttpClient();

                //Eliminar viaje del usuario
                string jsonData = JsonConvert.SerializeObject(DeleteFlight);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlDelete, content);
                string result = await response.Content.ReadAsStringAsync();

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
        #endregion
    }
}
