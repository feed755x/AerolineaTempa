using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AerolineaTempa.Interfaces;
using AerolineaTempa.ViewModels.Base;
using MvvmHelpers.Commands;

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
            await _navegationService.GoBackPop();
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

            }
            
        }
        #endregion
    }
}
