using System.Threading.Tasks;
using System.Windows.Input;
using AerolineaTempa.Interfaces;
using AerolineaTempa.ViewModels.Base;
using MvvmHelpers.Commands;

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
            if (!string.IsNullOrEmpty(Aerolinea)
                && !string.IsNullOrEmpty(Origen)
                && !string.IsNullOrEmpty(FechaSalida)
                //&& !string.IsNullOrEmpty(HoraSalida.ToString())
                //&& !string.IsNullOrEmpty(HoraLlegada.ToString())
                && !string.IsNullOrEmpty(FechaLlegada)
                && !string.IsNullOrEmpty(Destino)
                && AsientosDisponibles > 0
                && PrecioAsiento > 0)
            {
                await _navegationService.GoBackPop();
            }
            else
            {

            }
        }
        #endregion
    }
}
