using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AerolineaTempa.Interfaces;
using AerolineaTempa.Models;
using AerolineaTempa.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using MvvmHelpers.Commands;

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
                UserDialogs.Instance.ShowLoading("Analizando");

                var ListFlights = Container.Current.Services.GetRequiredService<UserProfileViewModel>().ListFlights;
                ListFlights.Remove(SelectedFlight);
                await Task.Delay(3000);
                UserDialogs.Instance.HideLoading();

                await _navegationService.GoBackPop();
            }
            else
            {

            }
        }
        #endregion
    }
}
